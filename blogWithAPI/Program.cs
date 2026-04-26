using blogWithAPI.DataAccessLayer.Abstract;
using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.BusinessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using blogWithAPI.Handlers;
using Microsoft.EntityFrameworkCore;
using blogWithAPI;
using blogWithAPI.DataAccessLayer.Concrete;
using blogWithAPI.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı ve Identity Servisleri
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(connectionString));

// 1. Rate Limiting (Hız Sınırlama) Ayarları
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("strict", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 10; // 10 saniyede en fazla 10 istek
        opt.QueueLimit = 0;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddCors(options =>
{
    var identityConfig = builder.Configuration.GetSection("Identity");
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(identityConfig["FrontendUrl"] ?? "*")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
        });
});

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// 2. İş Mantığı / Katman Servisleri
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogManager>();
builder.Services.AddScoped<IAuditService, AuditManager>();

// 3. IdentityServer Yapılandırması
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddAspNetIdentity<AppUser>()
    .AddDeveloperSigningCredential();

builder.Services.AddHttpClient("InsecureClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

builder.Services.AddHttpClient(); // Varsayılan client


// 4. JWT Bearer (API Koruması)
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var identityConfig = builder.Configuration.GetSection("Identity");
        options.Authority = identityConfig["Authority"];
        options.MetadataAddress = identityConfig["InternalMetadata"]; 
            
        options.Audience = "blogapi";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = identityConfig["Authority"],
            ValidateLifetime = true
        };

        // HATA AYIKLAMA İÇİN LOGLARI AÇIYORUZ
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Auth Hata: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token Başarılı: " + context.SecurityToken.Id);
                return Task.CompletedTask;
            }
        };
    });

// 5. Standart API Servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Global Hata Yönetimi
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 6. Middleware Boru Hattı
app.UseRateLimiter(); // Hız sınırlama aktif

// 2. Güvenlik Başlıkları (Security Headers)
if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // Sadece HTTPS zorla
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    await next();
});

// 3. Swagger Koruması (Sadece Geliştirme Ortamında Görünsün)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
