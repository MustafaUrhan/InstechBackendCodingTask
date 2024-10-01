using Claims.Domain.Entities;
using Claims.Domain.Repositories.Claims;
using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;

namespace Claims.Application.Features.Claims.Commands.InsertClaim;

public sealed class InsertClaimCommandHandler : IRequestHandler<InsertClaimCommand, ErrorOr<InsertClaimResponse>>
{
    private readonly ICoverQueryRepository _coverQueryRepository;
    private readonly IClaimCommandRepository _claimCommandRepository;
    public InsertClaimCommandHandler(ICoverQueryRepository coverQueryRepository, IClaimCommandRepository claimCommandRepository)
    {
        _coverQueryRepository = coverQueryRepository;
        _claimCommandRepository = claimCommandRepository;
    }

    public async Task<ErrorOr<InsertClaimResponse>> Handle(InsertClaimCommand request, CancellationToken cancellationToken)
    {
        var cover = await _coverQueryRepository.GetSingleAsync(predicate: cover => cover.Id == request.CoverId,
                                                                     cancellationToken: cancellationToken);
        if (cover is null)
        {
            return Errors.Business.ResultNotFound($"Cover with id {request.CoverId} not found");
        }

        if (request.Created < cover.StartDate || request.Created > cover.EndDate)
        {
            return Errors.Validation.ValidationFailed("Claim date is not within the cover period");
        }

        var newClaim = new Claim()
        {
            CoverId = request.CoverId,
            Created = request.Created,
            Name = request.Name,
            Type = request.Type,
            DamageCost = request.DamageCost
        };
        await _claimCommandRepository.AddAsync(newClaim, cancellationToken);
        //TODO: Audit record should be send to the audit service
        return new InsertClaimResponse(newClaim.Id, newClaim.CoverId, newClaim.Created, newClaim.Name, (int)newClaim.Type, newClaim.DamageCost);
    }
}