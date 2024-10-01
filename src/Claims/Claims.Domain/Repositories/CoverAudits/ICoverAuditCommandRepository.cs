namespace Claims.Domain.Repositories.CoverAudits;

public interface ICoverAuditCommandRepository
{
    Task AddAsync(string id, string httpRequestType);
}