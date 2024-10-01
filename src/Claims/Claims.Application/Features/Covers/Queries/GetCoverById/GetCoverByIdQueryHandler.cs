using Claims.Domain.Repositories.Claims;
using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Covers.Queries.GetCoverById;

public sealed class GetCoverByIdQueryHandler : IRequestHandler<GetCoverByIdQuery, ErrorOr<GetCoverByIdResponse>>
{
    private readonly ICoverQueryRepository _coverQueryRepository;

    public GetCoverByIdQueryHandler(ICoverQueryRepository coverQueryRepository)
    {
        _coverQueryRepository = coverQueryRepository;
    }

    public async Task<ErrorOr<GetCoverByIdResponse>> Handle(GetCoverByIdQuery request, CancellationToken cancellationToken)
    {
        var cover = await _coverQueryRepository.GetSingleAsync(selector: c => new GetCoverByIdResponse(c.Id,
                                                                                                     c.StartDate,
                                                                                                     c.EndDate,
                                                                                                     (int)c.Type,
                                                                                                     c.Premium,
                                                                                                     c.CreatedAt),
                                                             predicate: c => c.Id == request.Id);

        if (cover is null)
        {
            return Errors.Business.ResultNotFound($"Cover with id {request.Id} not found");
        }

        return cover;
    }
}