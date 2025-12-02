using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {
        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { get; }

        public string Description { get;  }
        public ErrorType ErrorType { get; }

        // Factory method to create an Error instance
        #region Static factory methods

        public static Error Failure(string code = "General.Failure", string Description = "A general failure has occurred.")
        {
            return new Error(code, Description, ErrorType.Failure);
        }
        public static Error ValidationError(string code= "General.ValidationError", string description="A validation error has occurred.")
        {
            return new Error(code, description, ErrorType.ValidationError);
        }
        public static Error NotFound(string code="General.Notfound", string description="The requested resource not found.")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error UnAuthorized(string code = "General.Unauthorized", string description = "You are not authorized to perform this action.")
        {
            return new Error(code, description, ErrorType.unAuthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string description = "You do not have permission to access this resource.")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCrendentials(string code = "General.InvalidCredentials", string description = "The provided credentials are invalid.")
        {
            return new Error(code, description, ErrorType.InvalidCrendentials);
        } 
        #endregion
    }
}
