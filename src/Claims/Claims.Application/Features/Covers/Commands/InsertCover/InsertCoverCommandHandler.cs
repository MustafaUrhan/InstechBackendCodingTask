using Claims.Application.Features.Covers.Notifications.CreateOrDeleteClaim;
using Claims.Application.Features.Covers.Services;
using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;


namespace Claims.Application.Features.Covers.Commands.InsertCover;

public sealed class InsertCoverCommandHandler : IRequestHandler<InsertCoverCommand, ErrorOr<InsertCoverResponse>>
{
    private readonly ICoverCommandRepository _coverCommandRepository;
    private readonly IPublisher _publisher;
    public InsertCoverCommandHandler(ICoverCommandRepository coverCommandRepository, IPublisher publisher)
    {
        _coverCommandRepository = coverCommandRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<InsertCoverResponse>> Handle(InsertCoverCommand request, CancellationToken cancellationToken)
    {
        if (!CoverTypeExtensions.IsValid(request.Type))
        {
            return Errors.Business.InvalidData("Invalid cover type");
        }

        var calculator = PremiumCalculatorFactory.GetCalculator(request.Type);
        var premium = calculator.CalculatePremium(request.StartDate, request.EndDate);
        var newCover = new Cover()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Type = request.Type,
            Premium = premium
        };

        await _coverCommandRepository.AddAsync(newCover);

        var coverResponse = new InsertCoverResponse(newCover.Id,
                                         newCover.StartDate,
                                         newCover.EndDate,
                                         (int)newCover.Type,
                                         newCover.Premium,
                                         newCover.CreatedAt);
        //TODO: Audit record should be send to the audit service
        var auditNotification = new CreateOrDeleteCoverNotification(newCover.Id.ToString(), "POST");
        await _publisher.Publish(auditNotification);
        return coverResponse;
    }
}
