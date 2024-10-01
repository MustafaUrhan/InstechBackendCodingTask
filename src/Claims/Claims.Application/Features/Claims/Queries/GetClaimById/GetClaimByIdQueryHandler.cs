using Claims.Domain.Repositories.Claims;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Claims.Queries.GetClaimById;

public sealed class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ErrorOr<GetClaimByIdResponse>>
{
    private readonly IClaimQueryRepository _claimQueryRepository;

    public GetClaimByIdQueryHandler(IClaimQueryRepository claimQueryRepository)
    {
        _claimQueryRepository = claimQueryRepository;
    }

    public async Task<ErrorOr<GetClaimByIdResponse>> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimQueryRepository.GetSingleAsync(selector: s => new GetClaimByIdResponse(s.Id,
                                                                                                       s.CoverId,
                                                                                                       s.Created,
                                                                                                       s.Name,
                                                                                                       (int)s.Type,
                                                                                                       s.DamageCost),
                                                             predicate: s => s.Id == request.Id,
                                                             cancellationToken: cancellationToken);
        if (claim == null)
        {
            return Errors.Business.ResultNotFound($"Claim with id {request.Id} not found");
        }
        return claim;
    }
}