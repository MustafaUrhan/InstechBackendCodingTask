using Claims.Domain.Repositories.ClaimAudits;
using Claims.Domain.Repositories.Claims;
using Claims.Domain.Repositories.CoverAudits;
using Claims.Domain.Repositories.Covers;
using Claims.Infrastructure.Persistence.Contexts;
using Claims.Infrastructure.Repositories.ClaimAudits;
using Claims.Infrastructure.Repositories.Claims;
using Claims.Infrastructure.Repositories.CoverAudits;
using Claims.Infrastructure.Repositories.Covers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.Database;

namespace Claims.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddScoped<IClaimQueryRepository, ClaimQueryRepository>();
        services.AddScoped<IClaimCommandRepository, ClaimCommandRepository>();
        services.AddScoped<ICoverQueryRepository, CoverQueryRepository>();
        services.AddScoped<ICoverCommandRepository, CoverCommandRepository>();
        services.AddScoped<ICoverAuditCommandRepository, CoverAuditCommandRepository>();
        services.AddScoped<IClaimAuditCommandRepository, ClaimAuditCommandRepository>();

        services.AddSqlServers<ClaimsDbContext>();
        services.AddSingleton<AuditDbContext>(provider => new AuditDbContext(Configuration.GetConnectionString("MongoDb")));
        return services;
    }
}