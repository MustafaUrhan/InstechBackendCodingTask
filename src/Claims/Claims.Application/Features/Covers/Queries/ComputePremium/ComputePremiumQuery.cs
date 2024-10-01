using Claims.Domain.Enums;
using ErrorOr;
using MediatR;

namespace Claims.Application.Features.Covers.Queries.ComputePremium;

public sealed record ComputePremiumQuery(DateTime StartDate,
                                           DateTime EndDate,
                                           CoverType Type) : IRequest<ErrorOr<decimal>>;