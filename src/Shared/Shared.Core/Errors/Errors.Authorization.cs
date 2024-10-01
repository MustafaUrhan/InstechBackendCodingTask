using ErrorOr;

namespace Shared.Core.Errors;

public static partial class Errors
{
    public static class Authorization
    {
        public static Error ClaimNotFound(string message)
        {
            return Error.Failure(code: "ClaimNotFound", message);
        }

        public static Error NotAuthorized(string message)
        {
            return Error.Failure(code: "NotAuthorizedException", message);
        }

        public static Error UserNotFound(string message)
        {
            return Error.Failure(code: "UserNotFound", message);
        }

        public static Error UserAlreadyExists(string message)
        {
            return Error.Failure(code: "UserAlreadyExists", message);
        }
    }
}