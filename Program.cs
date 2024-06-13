using asp_net_ecommerce_api.Configurations;
using asp_net_ecommerce_api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce API",
        Description = "Buy the products you love",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (connectionString != null)
    {
        options.UseMySQL(connectionString);
    }
    else
    {
        Console.WriteLine("Error: Connection string is null.");
    }
});

// Add or register repositories
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure JWT authentication
var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);

var jwtSettings = jwtSection.Get<JwtSettings>();
if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
{
    Console.WriteLine("Error: JWT settings are not configured properly.");
    throw new InvalidOperationException("JWT settings are not configured properly.");
}

Console.WriteLine($"JWT Key: {jwtSettings.Key}");
Console.WriteLine($"JWT Issuer: {jwtSettings.Issuer}");
Console.WriteLine($"JWT Audience: {jwtSettings.Audience}");
Console.WriteLine($"JWT ExpiresInMinutes: {jwtSettings.ExpiresInMinutes}");

var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Should be true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Set clock skew to zero to prevent token expiration issues
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1");
    });
}

// Init authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
