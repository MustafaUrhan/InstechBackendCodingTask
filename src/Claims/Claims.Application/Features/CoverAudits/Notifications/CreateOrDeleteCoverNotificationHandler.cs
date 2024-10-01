using Claims.Application.Features.Covers.Notifications.CreateOrDeleteClaim;
using Claims.Domain.Repositories.CoverAudits;
using MediatR;

namespace Claims.Application.Features.CoverAudits.Notifications;

public sealed class CreateOrDeleteCoverNotificationHandler: INotificationHandler<CreateOrDeleteCoverNotification>
{
    private readonly ICoverAuditCommandRepository _coverAuditCommandRepository;

    public CreateOrDeleteCoverNotificationHandler(ICoverAuditCommandRepository coverAuditCommandRepository)
    {
        _coverAuditCommandRepository = coverAuditCommandRepository;
    }

    public async Task Handle(CreateOrDeleteCoverNotification notification, CancellationToken cancellationToken)
    {
        await _coverAuditCommandRepository.AddAsync(notification.Id, notification.HttpRequestType);
    }
}