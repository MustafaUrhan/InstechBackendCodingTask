using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Covers.Queries.GetCoverById;

public sealed record GetCoverByIdQuery(Guid Id) : IRequest<ErrorOr<GetCoverByIdResponse>>;