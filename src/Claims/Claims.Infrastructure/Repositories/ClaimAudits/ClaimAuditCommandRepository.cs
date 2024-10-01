using Claims.Domain.Entities;
using Claims.Domain.Repositories.ClaimAudits;
using Claims.Infrastructure.Persistence.Contexts;

namespace Claims.Infrastructure.Repositories.ClaimAudits;

public class ClaimAuditCommandRepository : IClaimAuditCommandRepository
{
    private readonly AuditDbContext _auditContext;

    public ClaimAuditCommandRepository(AuditDbContext auditContext)
    {
        _auditContext = auditContext;
    }

    public async Task AddAsync(string id, string httpRequestType)
    {
        var claimAudit = new ClaimAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                ClaimId = id
            };

            await _auditContext.ClaimAudits.InsertOneAsync(claimAudit);
    }
}