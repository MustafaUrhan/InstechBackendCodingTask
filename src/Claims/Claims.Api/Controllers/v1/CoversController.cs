using Claims.Application.Features.Covers.Commands.DeleteCover;
using Claims.Application.Features.Covers.Commands.InsertCover;
using Claims.Application.Features.Covers.Queries.ComputePremium;
using Claims.Application.Features.Covers.Queries.GetAllCovers;
using Claims.Application.Features.Covers.Queries.GetCoverById;
using Claims.Contracts.Covers;
using Claims.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Controllers;

namespace Claims.Api.Controllers.v1;

[Route("api/v1/[controller]")]
public class CoversController : BaseController
{
    public CoversController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCoverRequest request)
    {
        var command = new InsertCoverCommand(request.StartDate, request.EndDate, (CoverType)request.Type);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var command = new GetCoverByIdQuery(id);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var command = new GetAllCoversQuery();
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpPost("Compute")]
    public async Task<IActionResult> ComputePremiumAsync([FromBody] ComputePremiumRequest request)
    {
        var query = new ComputePremiumQuery(request.StartDate, request.EndDate, (CoverType)request.Type);
        var result = await _sender.Send(query);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteCoverCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

}