using Claims.Application.Features.Covers.Services;
using Claims.Domain.Enums;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Covers.Queries.ComputePremium;


public sealed class ComputePremiumQueryHandler : IRequestHandler<ComputePremiumQuery, ErrorOr<decimal>>
{
    public async Task<ErrorOr<decimal>> Handle(ComputePremiumQuery request, CancellationToken cancellationToken)
    {
         if (!CoverTypeExtensions.IsValid(request.Type))
        {
            return Errors.Business.InvalidData("Invalid cover type");
        }
        
        var calculator = PremiumCalculatorFactory.GetCalculator(request.Type);
        var premium = calculator.CalculatePremium(request.StartDate, request.EndDate);
        return premium;
    }
}