using Claims.Domain.Entities;
using Shared.Core.Persistence;

namespace Claims.Domain.Repositories.Claims;

public interface IClaimCommandRepository: ICommandRepository<Claim>
{
}