using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.Database;

namespace Shared.Persistence.Services;

public static class Dependencies
{
    public static IServiceCollection AddSharedPersistenceFramework(this IServiceCollection services,ConfigurationManager configurationManager)
    {
        services.AddSqlServers(configurationManager);
        return services;
    }
}