using blogWithAPI.DataAccessLayer.Abstract;
using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.BusinessLayer.Concrate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using blogWithAPI.Handlers;
using Microsoft.EntityFrameworkCore;
using blogWithAPI;
using blogWithAPI.DataAccessLayer.Concrete;
using blogWithAPI.Entity.Concrete;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı ve Identity Servisleri
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// 2. İş Mantığı / Katman Servisleri
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogManager>();

// 3. IdentityServer Yapılandırması
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddAspNetIdentity<AppUser>()
    .AddDeveloperSigningCredential();

builder.Services.AddHttpClient();

// 4. JWT Bearer (API Koruması)
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Environment.IsDevelopment() 
            ? "http://localhost:5279" 
            : "http://blog.mtapi.com.tr";
            
        options.Audience = "blogapi";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = builder.Environment.IsDevelopment() ? "http://localhost:5279" : "http://blog.mtapi.com.tr",
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

// Global Hata Yönetimi
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 6. Middleware Boru Hattı
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
