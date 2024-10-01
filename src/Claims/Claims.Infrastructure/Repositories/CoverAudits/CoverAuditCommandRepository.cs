using Claims.Domain.Entities;
using Claims.Domain.Repositories.CoverAudits;
using Claims.Infrastructure.Persistence.Contexts;

namespace Claims.Infrastructure.Repositories.CoverAudits;

public class CoverAuditCommandRepository : ICoverAuditCommandRepository
{
    private readonly AuditDbContext _auditContext;

    public CoverAuditCommandRepository(AuditDbContext auditContext)
    {
        _auditContext = auditContext;
    }

    public async Task AddAsync(string id, string httpRequestType)
    {
        var coverAudit = new CoverAudit()
        {
            Created = DateTime.Now,
            HttpRequestType = httpRequestType,
            CoverId = id
        };

        await _auditContext.CoverAudits.InsertOneAsync(coverAudit);
    }
}