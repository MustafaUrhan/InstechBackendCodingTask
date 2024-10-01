using Claims.Domain.Entities;
using Claims.Domain.Repositories.Claims;
using Claims.Infrastructure.Persistence.Contexts;
using Shared.Persistence.Repositories.EntityFramework;

namespace Claims.Infrastructure.Repositories.Claims;

public class ClaimQueryRepository:QueryRepositoryBase<Claim,ClaimsDbContext>,IClaimQueryRepository
{
    public ClaimQueryRepository(ClaimsDbContext dbContext) : base(dbContext)
    {
    }
}