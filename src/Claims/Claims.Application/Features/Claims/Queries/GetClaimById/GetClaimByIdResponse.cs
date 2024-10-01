namespace Claims.Application.Features.Claims.Queries.GetClaimById;

public sealed record GetClaimByIdResponse(Guid ClaimId,
                                          Guid CoverId,
                                          DateTime Created,
                                          string Name,
                                          int Type,
                                          decimal DamageCost);