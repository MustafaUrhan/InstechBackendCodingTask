using Claims.Application.Features.Claims.Notifications.CreateOrDeleteClaim;
using Claims.Domain.Repositories.ClaimAudits;
using MediatR;

namespace Claims.Application.Features.ClaimAudits.Notifications;

public sealed class CreateOrDeleteClaimNotificationHandler : INotificationHandler<CreateOrDeleteClaimNotification>
{
    private readonly IClaimAuditCommandRepository _claimAuditCommandRepository;

    public CreateOrDeleteClaimNotificationHandler(IClaimAuditCommandRepository claimAuditCommandRepository)
    {
        _claimAuditCommandRepository = claimAuditCommandRepository;
    }

    public async Task Handle(CreateOrDeleteClaimNotification notification, CancellationToken cancellationToken)
    {
        await _claimAuditCommandRepository.AddAsync(notification.Id, notification.HttpRequestType);
    }
}