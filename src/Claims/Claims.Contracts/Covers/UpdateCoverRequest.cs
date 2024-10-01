namespace Claims.Contracts.Covers;

public sealed record UpdateCoverRequest(DateTime StartDate,
                                        DateTime EndDate,
                                        int Type);