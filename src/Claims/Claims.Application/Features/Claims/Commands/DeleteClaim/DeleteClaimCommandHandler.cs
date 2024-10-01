using Claims.Application.Features.Claims.Notifications.CreateOrDeleteClaim;
using Claims.Domain.Repositories.Claims;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Claims.Commands.DeleteClaim;

public sealed class DeleteClaimCommandHandler: IRequestHandler<DeleteClaimCommand, ErrorOr<Guid>>
{
    private readonly IClaimCommandRepository _claimCommandRepository;
    private readonly IClaimQueryRepository _claimQueryRepository;

    private readonly IPublisher _publisher;
    public DeleteClaimCommandHandler(IClaimCommandRepository claimCommandRepository, IClaimQueryRepository claimQueryRepository, IPublisher publisher)
    {
        _claimCommandRepository = claimCommandRepository;
        _claimQueryRepository = claimQueryRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<Guid>> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _claimQueryRepository.GetSingleAsync(predicate: s => s.Id == request.Id,
                                                               cancellationToken: cancellationToken);
        if (claim is null)
        {
            return Errors.Business.ResultNotFound($"Claim with id {request.Id} not found");
        }

        claim.IsActive = false;
        claim.IsDeleted = true;
        claim.ModifiedAt = DateTime.UtcNow;
        await _claimCommandRepository.UpdateAsync(claim, cancellationToken);

        var auditNotification = new CreateOrDeleteClaimNotification(claim.Id.ToString(), "DELETE");
        await _publisher.Publish(auditNotification);
        
        return request.Id;
    }
}