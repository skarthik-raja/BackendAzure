using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SkillAssessment.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ErrorController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred",
                Detail = _environment.IsDevelopment() ? exception?.StackTrace : exception?.Message
            };

            // Remove the exception stack trace and API URL from the response in non-production environment
            if (!_environment.IsProduction())
            {
                problemDetails.Extensions.Remove("traceId");
                problemDetails.Extensions.Remove("source");
            }

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }
    }
}
