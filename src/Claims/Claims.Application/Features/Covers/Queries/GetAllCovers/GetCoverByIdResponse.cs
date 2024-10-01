namespace Claims.Application.Features.Covers.Queries.GetAllCovers;

public sealed record GetAllCoversResponse(Guid Id,
                                          DateTime StartDate,
                                          DateTime EndDate,
                                          int Type,
                                          decimal Premium,
                                          DateTime CreatedAt);