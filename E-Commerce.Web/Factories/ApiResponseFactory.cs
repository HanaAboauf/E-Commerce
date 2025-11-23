using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState.Where(E => E.Value.Errors.Count > 0)
                        .ToDictionary(x => x.Key, x => x.Value.Errors.Select(em => em.ErrorMessage).ToArray());
            var problem = new ProblemDetails()
            {
                Title = "Validation error",
                Detail = "One or more validation error occured",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            { "errors", Errors }
                        }
            };
            return new BadRequestObjectResult(problem);

        }
    }
}
