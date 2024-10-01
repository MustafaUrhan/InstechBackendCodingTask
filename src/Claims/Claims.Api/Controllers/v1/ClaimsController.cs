using Claims.Application.Features.Claims.Commands.DeleteClaim;
using Claims.Application.Features.Claims.Commands.InsertClaim;
using Claims.Application.Features.Claims.Queries.GetAllClaims;
using Claims.Application.Features.Claims.Queries.GetClaimById;
using Claims.Contracts.Claims;
using Claims.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Controllers;

namespace Claims.Api.Controllers.v1;

[Route("api/v1/[controller]")]
public class ClaimsController : BaseController
{
    public ClaimsController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateClaimRequest request)
    {
        var command = new InsertClaimCommand(request.CoverId, request.Created, request.Name, (ClaimType)request.Type, request.DamageCost);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var query = new GetClaimByIdQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var query = new GetAllClaimsQuery();
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteClaimCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }
}