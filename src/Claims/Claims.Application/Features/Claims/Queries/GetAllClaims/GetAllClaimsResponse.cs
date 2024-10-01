namespace Claims.Application.Features.Claims.Queries.GetAllClaims;

public sealed record GetAllClaimsResponse(Guid ClaimId,
                                          Guid CoverId,
                                          DateTime Created,
                                          string Name,
                                          int Type,
                                          decimal DamageCost);