using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Claims.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
          services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Dependencies).Assembly));
        return services;
    }
}