using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string code, string description, ErrorType type = ErrorType.Failure)
    {
        public static Error Failure(string code="General.Failure", string description="An unexpected error occurred.") => new(code, description, ErrorType.Failure);
        public static Error Validation(string code="General.Validation", string description="The provided data is invalid.") => new(code, description, ErrorType.Validation);
        public static Error NotFound(string code="General.NotFound", string description="The requested resource was not found.") => new(code, description, ErrorType.NotFound);
        public static Error Conflict(string code="General.Conflict", string description="A conflict occurred while processing the request.") => new(code, description, ErrorType.Conflict);
        public static Error Unauthorized(string code="General.Unauthorized", string description="The requested resource requires authentication.") => new(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code="General.Forbidden", string description="The requested resource is forbidden.") => new(code, description, ErrorType.Forbidden);
        public static Error Invalidcredentails(string code="General.InvalidCredentails", string description="The provided credentials are invalid.") => new(code, description, ErrorType.InvalidCredentails);
    }
}
