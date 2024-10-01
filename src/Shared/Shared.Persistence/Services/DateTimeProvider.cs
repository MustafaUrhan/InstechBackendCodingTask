namespace Shared.Persistence.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUtc => DateTime.UtcNow;
}
