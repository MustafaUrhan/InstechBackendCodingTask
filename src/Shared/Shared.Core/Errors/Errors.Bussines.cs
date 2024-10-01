using ErrorOr;
using Shared.Core.Errors;

namespace Shared.Application.Concerns.Exceptions;

public static partial class Errors
{

    public static class Business
    {
        public static Error BusinessError(string message)
        {
            return Error.Failure(code: "BusinessException", message);
        }

        public static Error ResultNotFound(string message)
        {
            return Error.NotFound(code: "ResultNotFound", message);
        }

        public static Error EntityCouldNotInsert(string message)
        {
            return Error.Failure(code: "EntityNotInserted", message);
        }

        public static Error EntityAlreadyExists(string message)
        {
            return Error.Failure(code: "EntityAlreadyExists", message);
        }

        public static Error InvalidData(string message)
        {
            return Error.Failure(code: "InvalidData", message);
        }
    }
}