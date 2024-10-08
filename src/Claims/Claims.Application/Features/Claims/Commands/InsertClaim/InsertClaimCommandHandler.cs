using Claims.Application.Features.Claims.Notifications.CreateOrDeleteClaim;
using Claims.Domain.Entities;
using Claims.Domain.Repositories.Claims;
using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Claims.Commands.InsertClaim;

public sealed class InsertClaimCommandHandler : IRequestHandler<InsertClaimCommand, ErrorOr<InsertClaimResponse>>
{
    private readonly ICoverQueryRepository _coverQueryRepository;
    private readonly IClaimCommandRepository _claimCommandRepository;
    private readonly IPublisher _publisher;
    public InsertClaimCommandHandler(ICoverQueryRepository coverQueryRepository, IClaimCommandRepository claimCommandRepository, IPublisher publisher)
    {
        _coverQueryRepository = coverQueryRepository;
        _claimCommandRepository = claimCommandRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<InsertClaimResponse>> Handle(InsertClaimCommand request, CancellationToken cancellationToken)
    {
        var cover = await _coverQueryRepository.GetSingleAsync(predicate: cover => cover.Id == request.CoverId,
                                                                     cancellationToken: cancellationToken);
        if (cover is null)
        {
            return Errors.Business.ResultNotFound($"Cover with id {request.CoverId} not found");
        }

        var isValidDate = request.Created >= cover.StartDate && request.Created <= cover.EndDate;
        if (!isValidDate)
        {
            return Errors.Validation.ValidationFailed("Claim date is not within the cover period");
        }

        var newClaim = new Claim()
        {
            CoverId = request.CoverId,
            Created = request.Created,
            Name = request.Name,
            Type = request.Type,
            DamageCost = request.DamageCost
        };
        await _claimCommandRepository.AddAsync(newClaim, cancellationToken);
        
        var auditNotification = new CreateOrDeleteClaimNotification(newClaim.Id.ToString(), "POST");
        await _publisher.Publish(auditNotification);
        
        return new InsertClaimResponse(newClaim.Id, newClaim.CoverId, newClaim.Created, newClaim.Name, (int)newClaim.Type, newClaim.DamageCost);
    }
}