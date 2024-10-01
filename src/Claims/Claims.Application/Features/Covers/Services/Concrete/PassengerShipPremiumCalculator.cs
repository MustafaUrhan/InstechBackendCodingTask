using Claims.Application.Features.Covers.Helpers;
using Claims.Domain.Enums;
using Claims.Domain.Statics;

namespace Claims.Application.Features.Covers.Services.Concrete;

public class PassengerShipPremiumCalculator  : IPremiumCalculator
{
    public decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        var passengerTypePremium = CoverTypePremiums.GetPremium(CoverType.PassengerShip);
        return ComputePremiumCalculator.ComputePremium(startDate, endDate, passengerTypePremium, CoverType.PassengerShip);
    }
}