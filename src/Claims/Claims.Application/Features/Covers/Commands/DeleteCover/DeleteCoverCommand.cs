using ErrorOr;
using MediatR;
using Shared.Application.Logging;

namespace Claims.Application.Features.Covers.Commands.DeleteCover;


public sealed record DeleteCoverCommand(Guid Id) : IRequest<ErrorOr<Guid>>,ILoggableRequest;