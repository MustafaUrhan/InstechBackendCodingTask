using MediatR;

namespace Claims.Application.Features.Covers.Notifications.CreateOrDeleteClaim;

public sealed record CreateOrDeleteCoverNotification(string Id, string HttpRequestType) : INotification;