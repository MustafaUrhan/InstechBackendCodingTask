using Claims.Domain.Entities;
using MongoDB.Driver;

namespace Claims.Infrastructure.Persistence.Contexts;

public class AuditDbContext
{
    private readonly IMongoDatabase _database;

    public AuditDbContext(string connectionString)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("AuditDb");
    }

    public IMongoCollection<ClaimAudit> ClaimAudits => _database.GetCollection<ClaimAudit>("ClaimAudits");
    public IMongoCollection<CoverAudit> CoverAudits => _database.GetCollection<CoverAudit>("CoverAudits");

}