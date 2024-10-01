using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.Constants;

namespace Shared.Persistence.Database;

public static class Extensions
{
    public static IServiceCollection AddSqlServers(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddHostedService<DbContextAppInitializer>();
        return services;
    }

    public static IServiceCollection AddSqlServers<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration[ConnectionStrings.SqlServer];
        services.AddDbContext<T>(x => x.UseSqlServer(connectionString).LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));

        return services;
    }
}
