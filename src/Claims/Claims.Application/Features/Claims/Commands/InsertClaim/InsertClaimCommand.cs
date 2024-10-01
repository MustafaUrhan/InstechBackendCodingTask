using Claims.Domain.Enums;
using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Claims.Commands.InsertClaim;

public sealed record InsertClaimCommand(Guid CoverId,
                                        DateTime Created,
                                        string Name,
                                        ClaimType Type,
                                        decimal DamageCost) : IRequest<ErrorOr<InsertClaimResponse>>;