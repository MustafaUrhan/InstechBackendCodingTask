using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Covers.Queries.GetAllCovers;

public sealed class GetAllCoversQueryHandler : IRequestHandler<GetAllCoversQuery, ErrorOr<IEnumerable<GetAllCoversResponse>>>
{
    private readonly ICoverQueryRepository _coverQueryRepository;

    public GetAllCoversQueryHandler(ICoverQueryRepository coverQueryRepository)
    {
        _coverQueryRepository = coverQueryRepository;
    }

    public async Task<ErrorOr<IEnumerable<GetAllCoversResponse>>> Handle(GetAllCoversQuery request, CancellationToken cancellationToken)
    {
        var coverList = await _coverQueryRepository.GetListAsync(selector: c => new GetAllCoversResponse(c.Id,
                                                                                                     c.StartDate,
                                                                                                     c.EndDate,
                                                                                                     (int)c.Type,
                                                                                                     c.Premium,
                                                                                                     c.CreatedAt),
                                                             cancellationToken: cancellationToken);
        return coverList;
    }
}