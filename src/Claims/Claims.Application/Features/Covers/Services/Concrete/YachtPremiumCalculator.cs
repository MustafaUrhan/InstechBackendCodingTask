using Claims.Application.Features.Covers.Helpers;
using Claims.Domain.Enums;
using Claims.Domain.Statics;

namespace Claims.Application.Features.Covers.Services.Concrete;

public class YachtPremiumCalculator : IPremiumCalculator
{
    public decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        var yachtPremium=CoverTypePremiums.GetPremium(CoverType.Yacht);
        return ComputePremiumCalculator.ComputePremium(startDate, endDate, yachtPremium, CoverType.Yacht);
    }


}