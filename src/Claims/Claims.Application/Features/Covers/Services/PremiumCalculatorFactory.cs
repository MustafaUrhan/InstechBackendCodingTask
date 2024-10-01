using Claims.Application.Features.Covers.Services.Concrete;
using Claims.Domain.Enums;

namespace Claims.Application.Features.Covers.Services;

public class PremiumCalculatorFactory
{
    public static IPremiumCalculator GetCalculator(CoverType coverType)
    {
        return coverType switch
        {
            CoverType.Yacht => new YachtPremiumCalculator(),
            CoverType.PassengerShip => new PassengerShipPremiumCalculator(),
            CoverType.Tanker => new TankerPremiumCalculator(),
            _ => new DefaultPremiumCalculator(),
        };
    }
}