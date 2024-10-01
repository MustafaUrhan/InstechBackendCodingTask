using Claims.Domain.Enums;

namespace Claims.Domain.Statics;

public static class CoverTypePremiums
{
    public static decimal GetPremium(CoverType? coverType=null)
    {
        if (coverType == null)
        {
            return 1.3m;
        }

        return coverType switch
        {
            CoverType.Yacht => 1.1m,
            CoverType.PassengerShip => 1.2m,
            CoverType.Tanker => 1.5m,
            _ => 1.3m
        };
    }
}
