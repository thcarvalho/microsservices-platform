using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MSP.Auth.API;
using MSP.Clients.API.Data;
using MSP.Clients.API.Integration;
using MSP.Clients.API.Validations;
using MSP.Core.Extensions;
using MSP.MessageBus;
using Scrutor;

namespace MSP.Clients.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Scan(selector => selector
                .FromAssemblies(
                    AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());

        services.AddDbContext<ClientsContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger()
                .UseStaticFiles()
                .UseCors()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
        });

        return app;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.ConfigureSettings(configuration);

        services.AddControllers();

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClientRequestValidator>());

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
                .AllowAnyHeader()
            );
        });

        return services;
    }

    public static IServiceCollection ConfigureSettings(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        //services.Configure<TokenGeneratorSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
    public static IServiceCollection ConfigureMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<ClientIntegrationEventHandler>();

        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = $"MSP Clients Web Api",
                    Version = "v1",
                    Description = $"Project for MSP Clients ASP.Net Core"
                });


            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}