using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using NovaHub.Backend.Options;

namespace NovaHub.Backend.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerVersioning(this IServiceCollection services)
    {
        // ----------------------------
        // 1) Api Versioning
        // ----------------------------
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        
        // ----------------------------
        // 2) Explorer Versioning 
        // ----------------------------
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                        Array.Empty<string>()
                    }
                });
            })
            .AddSwaggerVersioning();
            
        

        return services;
    }

    public static IApplicationBuilder UseSwaggerDev(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var d in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{d.GroupName}/swagger.json", $"NovaHub {d.GroupName.ToUpperInvariant()}");
            }
        });

        return app;
    }
}