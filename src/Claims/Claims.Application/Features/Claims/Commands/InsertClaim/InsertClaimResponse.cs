namespace Claims.Application.Features.Claims.Commands.InsertClaim;

public sealed record InsertClaimResponse(Guid Id,
                                          Guid CoverId,
                                          DateTime Created,
                                          string Name,
                                          int Type,
                                          decimal DamageCost);