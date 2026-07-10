using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        public static ActionResult<T>ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.data);
            }
           return ToProblem(result.Errors);
        }
        public static ActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }
            return ToProblem(result.Errors);
        }

        private static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
          var First= errors[0];
            var status = First.type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = First.code,
                Detail = First.description,
                Extensions = { ["errors"] = errors }
   
            };
            return new OkObjectResult(problemDetails) { StatusCode = status };
        }
    }
}
