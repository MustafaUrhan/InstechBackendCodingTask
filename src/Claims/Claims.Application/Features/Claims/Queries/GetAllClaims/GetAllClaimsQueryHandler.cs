using Claims.Domain.Repositories.Claims;
using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Claims.Queries.GetAllClaims;

public sealed class GetAllClaimsQueryHandler: IRequestHandler<GetAllClaimsQuery, ErrorOr<IEnumerable<GetAllClaimsResponse>>>
{
    private readonly IClaimQueryRepository _claimQueryRepository;

    public GetAllClaimsQueryHandler(IClaimQueryRepository claimQueryRepository)
    {
        _claimQueryRepository = claimQueryRepository;
    }

    public async Task<ErrorOr<IEnumerable<GetAllClaimsResponse>>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        var claims = await _claimQueryRepository.GetListAsync(selector: s => new GetAllClaimsResponse(s.Id,
                                                                                                      s.CoverId,
                                                                                                      s.Created,
                                                                                                      s.Name,
                                                                                                      (int)s.Type,
                                                                                                      s.DamageCost),
                                                              cancellationToken: cancellationToken);
        return claims;
    }
}