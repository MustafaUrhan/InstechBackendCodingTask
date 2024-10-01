namespace Claims.Application.Features.Covers.Services;

public interface IPremiumCalculator
{
    decimal CalculatePremium(DateTime startDate, DateTime endDate);
}