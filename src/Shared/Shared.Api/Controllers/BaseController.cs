using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ISender _sender;

    protected BaseController(ISender sender)
    {
        _sender = sender;
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }

        HttpContext.Items["errors"] = errors;
        var statucCode = errors.First().Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statucCode, title: errors.First().Description);
    }
}