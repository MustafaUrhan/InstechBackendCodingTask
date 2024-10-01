using Claims.Domain.Enums;
using ErrorOr;
using MediatR;
using Shared.Application.Logging;

namespace Claims.Application.Features.Covers.Commands.InsertCover;
public sealed record InsertCoverCommand(DateTime StartDate,
                              DateTime EndDate,
                              CoverType Type) : IRequest<ErrorOr<InsertCoverResponse>>,ILoggableRequest;