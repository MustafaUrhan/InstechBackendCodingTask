using MediatR;

namespace Claims.Application.Features.Claims.Notifications.CreateOrDeleteClaim;

public sealed record CreateOrDeleteClaimNotification(string Id, string HttpRequestType) : INotification;