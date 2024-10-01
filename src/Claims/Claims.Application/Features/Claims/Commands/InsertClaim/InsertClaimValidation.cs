using FluentValidation;

namespace Claims.Application.Features.Claims.Commands.InsertClaim;


public class InsertClaimValidation : AbstractValidator<InsertClaimCommand>
{
    public InsertClaimValidation()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(command => command.DamageCost).LessThan(100000).WithMessage("Damage cost cannot exceed 100000");
    }
}