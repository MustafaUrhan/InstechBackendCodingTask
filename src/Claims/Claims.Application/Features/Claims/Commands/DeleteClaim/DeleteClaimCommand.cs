using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Claims.Commands.DeleteClaim;

public sealed record DeleteClaimCommand(Guid Id) : IRequest<ErrorOr<Guid>>;