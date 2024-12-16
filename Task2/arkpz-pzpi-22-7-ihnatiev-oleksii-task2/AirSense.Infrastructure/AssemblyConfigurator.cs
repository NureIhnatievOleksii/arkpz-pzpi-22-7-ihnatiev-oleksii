using System.Text;
using AirSense.Application.Interfaces.Repositories;
using AirSense.Application.Interfaces.Services;
using AirSense.Application.Options;
using AirSense.Application.Services;
using AirSense.Domain.UserAggregate;
using AirSense.Infrastructure.Database;
using AirSense.Infrastructure.Repositories;
using EmailService;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ApplicationAssemblyConfigurator = AirSense.Infrastructure.AssemblyConfigurator;

namespace AirSense.Infrastructure;

public static class AssemblyConfigurator
{
    private const string DevLibSqlServer = "DevLibSqlServer";

    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AirSenseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DevLibSqlServer)));

        services.AddAuthorization();

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        })
        .AddEntityFrameworkStores<AirSenseContext>()
        .AddDefaultTokenProviders();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        })
        .AddCookie()
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        });


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите 'Bearer' [пробел] и ваш токен внизу для доступа к защищённым ресурсам.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddHostedService<UserInitializerService>();

        return services
            .AddRepositories()
            .AddServices()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyConfigurator).Assembly));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<IEmailSender, EmailSender>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IJwtService, JwtService>();
    }

}
