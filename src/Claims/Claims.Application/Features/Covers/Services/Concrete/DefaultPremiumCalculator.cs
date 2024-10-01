using Claims.Application.Features.Covers.Helpers;
using Claims.Domain.Enums;
using Claims.Domain.Statics;

namespace Claims.Application.Features.Covers.Services.Concrete;

public class DefaultPremiumCalculator : IPremiumCalculator
{
    public decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        var defaultPremium = CoverTypePremiums.GetPremium();
        return ComputePremiumCalculator.ComputePremium(startDate, endDate, defaultPremium);
    }
}