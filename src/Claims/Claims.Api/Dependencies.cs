using System.Reflection;
using Claims.Application;
using Claims.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Shared.Api;
using Shared.Application;
using Shared.Persistence.Services;

namespace Claims.Api;

public static class Dependencies
{
    public static IServiceCollection AddModuleServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddApplicationLayer()
                .AddInfrastructureLayer(configuration)
                .AddSwagger()
                .AddVersioning();
        return services;
    }
    public static IServiceCollection AddSharedFrameworkServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSharedApplicationFramework(configuration,Assembly.GetExecutingAssembly())
                .AddSharedPersistenceFramework(configuration)
                .AddSharedApiFramework();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
            opt.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });
        });
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        return services;
    }


}