using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Claims.Queries.GetClaimById;

public sealed record GetClaimByIdQuery(Guid Id) : IRequest<ErrorOr<GetClaimByIdResponse>>;