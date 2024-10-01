using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Logging;
using Shared.Application.Validation;

namespace Shared.Application;

public static class Dependencies
{
    public static IServiceCollection AddSharedApplicationFramework(this IServiceCollection services, IConfiguration configuration,Assembly assembly)
    {
        services.AddHttpContextAccessor();
        AddPipelineBehaviors(services, configuration);
        return services;
    }

    private static void AddPipelineBehaviors(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}