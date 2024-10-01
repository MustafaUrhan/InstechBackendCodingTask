namespace Claims.Application.Features.Covers.Commands.InsertCover;

public sealed record InsertCoverResponse(Guid Id,
                                         DateTime StartDate,
                                         DateTime EndDate,
                                         int Type,
                                         decimal Premium,
                                         DateTime CreatedAt);