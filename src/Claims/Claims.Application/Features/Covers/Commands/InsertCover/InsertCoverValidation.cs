using FluentValidation;

namespace Claims.Application.Features.Covers.Commands.InsertCover;

public class InsertCoverValidation : AbstractValidator<InsertCoverCommand>
{
    public InsertCoverValidation()
    {
        RuleFor(command => command.StartDate).GreaterThan(DateTime.UtcNow)
                                             .WithMessage("Start date must be greater than current date");
                                             
        RuleFor(command => command.EndDate).GreaterThan(command => command.StartDate)
                                           .WithMessage("End date must be greater than start date");

        RuleFor(command => command.EndDate).LessThanOrEqualTo(command => command.StartDate.AddYears(1))
                                           .WithMessage("Insurance period cannot exceed 1 year");
    }
}