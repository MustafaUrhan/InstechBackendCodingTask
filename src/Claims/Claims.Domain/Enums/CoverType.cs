namespace Claims.Domain.Enums;

public enum CoverType
{
    Yacht = 0,
    PassengerShip = 1,
    ContainerShip = 2,
    BulkCarrier = 3,
    Tanker = 4
}

public static class CoverTypeExtensions
{
    public static bool IsValid( CoverType coverType)
    {
        return coverType >= CoverType.Yacht && coverType <= CoverType.Tanker;
    }
}

