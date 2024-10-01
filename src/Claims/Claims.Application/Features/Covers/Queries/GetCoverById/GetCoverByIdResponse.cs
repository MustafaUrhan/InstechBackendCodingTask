namespace Claims.Application.Features.Covers.Queries.GetCoverById;

public sealed record GetCoverByIdResponse(Guid Id,
                                          DateTime StartDate,
                                          DateTime EndDate,
                                          int Type,
                                          decimal Premium,
                                          DateTime CreatedAt);