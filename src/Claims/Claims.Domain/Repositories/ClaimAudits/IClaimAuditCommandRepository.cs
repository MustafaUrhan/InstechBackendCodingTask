namespace Claims.Domain.Repositories.ClaimAudits;

public interface IClaimAuditCommandRepository
{
    Task AddAsync(string id, string httpRequestType);
}