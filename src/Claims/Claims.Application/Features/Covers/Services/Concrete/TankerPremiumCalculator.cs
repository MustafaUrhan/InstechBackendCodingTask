using Claims.Application.Features.Covers.Helpers;
using Claims.Domain.Enums;
using Claims.Domain.Statics;

namespace Claims.Application.Features.Covers.Services.Concrete;


public class TankerPremiumCalculator : IPremiumCalculator
{
    public decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        var tankerTypePremium = CoverTypePremiums.GetPremium(CoverType.Tanker);
        return ComputePremiumCalculator.ComputePremium(startDate, endDate, tankerTypePremium, CoverType.Tanker);
    }
}