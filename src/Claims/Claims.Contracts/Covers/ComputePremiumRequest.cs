namespace Claims.Contracts.Covers;

public sealed record ComputePremiumRequest(DateTime StartDate, DateTime EndDate, int Type);