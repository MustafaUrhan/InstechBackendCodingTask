using Claims.Application.Features.Covers.Notifications.CreateOrDeleteClaim;
using Claims.Domain.Repositories.Covers;
using ErrorOr;
using MediatR;
using Shared.Application.Concerns.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Claims.Application.Features.Covers.Commands.DeleteCover;

public sealed class DeleteCoverCommandHandler : IRequestHandler<DeleteCoverCommand, ErrorOr<Guid>>
{

    private readonly ICoverCommandRepository _coverCommandRepository;
    private readonly ICoverQueryRepository _coverQueryRepository;
    private readonly IPublisher _publisher;
    public DeleteCoverCommandHandler(ICoverCommandRepository coverCommandRepository, ICoverQueryRepository coverQueryRepository, IPublisher publisher)
    {
        _coverCommandRepository = coverCommandRepository;
        _coverQueryRepository = coverQueryRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<Guid>> Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
    {
        var cover = await _coverQueryRepository.GetSingleAsync(predicate: s => s.Id == request.Id,
                                                               include: s => s.Include(x => x.Claims),
                                                               cancellationToken: cancellationToken);
        if (cover is null)
        {
            return Errors.Business.ResultNotFound($"Cover with id {request.Id} not found");
        }
        //TODO: Related claims should be deleted first

        cover.IsActive = false;
        cover.IsDeleted = true;
        cover.ModifiedAt = DateTime.UtcNow;
        // await _coverCommandRepository.UpdateAsync(cover, cancellationToken);
        // //TODO: Audit record should be send to the audit service
        //  var auditNotification = new CreateOrDeleteCoverNotification(cover.Id.ToString(), "DELETE");
        // await _publisher.Publish(auditNotification);
        return request.Id;
    }
}