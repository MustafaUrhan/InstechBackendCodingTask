using Claims.Domain.Entities;
using Claims.Domain.Repositories.Covers;
using Claims.Infrastructure.Persistence.Contexts;
using Shared.Persistence.Repositories.EntityFramework;

namespace Claims.Infrastructure.Repositories.Covers;

public class CoverCommandRepository : CommandRepositoryBase<Cover, ClaimsDbContext>, ICoverCommandRepository
{
    public CoverCommandRepository(ClaimsDbContext dbContext) : base(dbContext)
    {
    }
}
