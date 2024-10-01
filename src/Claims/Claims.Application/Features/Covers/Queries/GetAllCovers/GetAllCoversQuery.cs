using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Covers.Queries.GetAllCovers;

public sealed record GetAllCoversQuery : IRequest<ErrorOr<IEnumerable<GetAllCoversResponse>>>;