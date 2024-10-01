using Claims.Domain.Entities;
using Claims.Domain.Repositories.Claims;
using Claims.Infrastructure.Persistence.Contexts;
using Shared.Persistence.Repositories.EntityFramework;

namespace Claims.Infrastructure.Repositories.Claims;

public class ClaimCommandRepository : CommandRepositoryBase<Claim, ClaimsDbContext>, IClaimCommandRepository
{
    public ClaimCommandRepository(ClaimsDbContext dbContext) : base(dbContext)
    {
    }
}
