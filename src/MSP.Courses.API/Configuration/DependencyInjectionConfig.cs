﻿using Carter;
using Carter.OpenApi;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MSP.Courses.API.Features.Category.Validations;
using MSP.Courses.Infra;
using MSP.Courses.Infra.Data.Mappings;
using Scrutor;

namespace MSP.Courses.API.Configuration;

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

        CategoryMapping.Map();
        CourseMapping.Map();
        CourseLessonMapping.Map();
        CourseProfessorMapping.Map();
        CourseStudentMapping.Map();
        CourseStatusMapping.Map();

        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting()
            .UseSwagger()
            .UseStaticFiles()
            .UseCors()
            //.UseAuthentication()
            //.UseAuthorization()
            ;

        app.UseSwaggerUI();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
        });

        app.MapCarter();

        return app;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.ConfigureSettings(configuration);

        services.AddControllers();

        services.AddCarter();

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddCategoryRequestValidator>());

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();

        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

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

    //public static IServiceCollection ConfigureMessageBusConfiguration(this IServiceCollection services,
    //    IConfiguration configuration)
    //{
    //    services.ConfigureMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
    //        .AddHostedService<ClientIntegrationEventHandler>();

    //    return services;
    //}

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = $"MSP Courses Web Api",
                    Version = "v1",
                    Description = $"Project for MSP Courses ASP.Net Core"
                });

            options.DocInclusionPredicate((s, description) =>
            {
                foreach (var metaData in description.ActionDescriptor.EndpointMetadata)
                {
                    if (metaData is IIncludeOpenApi)
                    {
                        return true;
                    }
                }
                return false;
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