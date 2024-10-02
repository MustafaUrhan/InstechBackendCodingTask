using Claims.Domain.Enums;

namespace Claims.Domain.Statics;

public static class CoverTypePremiums
{
    public static decimal GetPremium(CoverType? coverType = null)
    {
        if (coverType == null)
        {
            return 1.3m + (1.3m * 0.3m);
        }

        return coverType switch
        {
            CoverType.Yacht => 1.1m + (1.1m * 0.1m),
            CoverType.PassengerShip => 1.2m + (1.2m * 0.2m),
            CoverType.Tanker => 1.5m + (1.5m * 0.5m),
            _ => 1.3m + (1.3m * 0.3m),
        };
    }
}
