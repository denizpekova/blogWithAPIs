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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null && connectionString.Contains("blog.db"))
{
    var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "blog.db");
    connectionString = $"Data Source={dbPath}";
}

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(connectionString));

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
    .AddAspNetIdentity<AppUser>()
    .AddDeveloperSigningCredential();

builder.Services.AddHttpClient();

// 4. JWT Bearer (API Koruması)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://blog.mtapi.com.tr";
        options.Audience = "blogapi";
        options.RequireHttpsMetadata = false;
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
