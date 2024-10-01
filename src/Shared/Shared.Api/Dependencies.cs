using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shared.Api.ErrorFactory;

namespace Shared.Api;

public static class Dependencies
{
    public static IServiceCollection AddSharedApiFramework(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        return services;
    }
}