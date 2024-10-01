namespace Claims.Contracts.Claims;

public record CreateClaimRequest(Guid CoverId,
                                 DateTime Created,
                                 string Name,
                                 int Type,
                                 decimal DamageCost);