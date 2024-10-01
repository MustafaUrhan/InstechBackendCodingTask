using Claims.Domain.Entities;
using Claims.Domain.Repositories.Covers;
using Claims.Infrastructure.Persistence.Contexts;
using Shared.Persistence.Repositories.EntityFramework;

namespace Claims.Infrastructure.Repositories.Covers;

public class CoverQueryRepository : QueryRepositoryBase<Cover, ClaimsDbContext>, ICoverQueryRepository
{
    public CoverQueryRepository(ClaimsDbContext dbContext) : base(dbContext)
    {
    }
}