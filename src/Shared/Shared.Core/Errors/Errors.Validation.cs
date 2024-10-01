using ErrorOr;
using Shared.Core.Errors;

namespace Shared.Application.Concerns.Exceptions;

public static partial class Errors
{
    public static class Validation
    {
        public static Error ValidationFailed(string message)
        {
            return Error.Validation(code: "ValidationException", message);
        }
    }
}