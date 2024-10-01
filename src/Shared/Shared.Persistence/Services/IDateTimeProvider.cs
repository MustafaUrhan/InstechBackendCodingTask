namespace Shared.Persistence.Services;

public interface IDateTimeProvider
{
    DateTime NowUtc { get; }
}