using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NovaHub.Backend.Options;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var d in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(d.GroupName, new OpenApiInfo
            {
                Title = $"NovaHub API {d.ApiVersion.ToString()}",
                Version = d.ApiVersion.ToString(),
                Description = $"Endpoints for NovaHub API {d.ApiVersion}"
            });
    }
}