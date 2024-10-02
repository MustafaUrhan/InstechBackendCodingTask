using Claims.Domain.Enums;

namespace Claims.Application.Features.Covers.Helpers;

public static class ComputePremiumCalculator
{
    public static decimal ComputePremium(DateTime startDate, DateTime endDate, decimal multiplier, CoverType? coverType = null)
    {
        var premiumPerDay = 1250 * multiplier;
        var insuranceLength = (endDate - startDate).TotalDays;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLength; i++)
        {
            if (i < 30) totalPremium += premiumPerDay;
            if (i >= 30 && i < 180) totalPremium += premiumPerDay - ((coverType != null && coverType == CoverType.Yacht) ? premiumPerDay * 0.05m : premiumPerDay * 0.02m);
            if (i >= 180 && i < 365) totalPremium += premiumPerDay - ((coverType != null && coverType == CoverType.Yacht) ? premiumPerDay * 0.03m : premiumPerDay * 0.01m);
        }

        return totalPremium;
    }
}