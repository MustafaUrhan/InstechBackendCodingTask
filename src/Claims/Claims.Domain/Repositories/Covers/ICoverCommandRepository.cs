using Claims.Domain.Entities;
using Shared.Core.Persistence;

namespace Claims.Domain.Repositories.Covers;

public interface ICoverCommandRepository : ICommandRepository<Cover>
{
}