using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Claims.Queries.GetAllClaims;

public sealed record GetAllClaimsQuery : IRequest<ErrorOr<IEnumerable<GetAllClaimsResponse>>>;