namespace Claims.Contracts.Covers;

public sealed record CreateCoverRequest(DateTime StartDate,
                                        DateTime EndDate,
                                        int Type);
