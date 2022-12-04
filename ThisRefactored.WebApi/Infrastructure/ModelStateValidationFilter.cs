using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ThisRefactored.WebApi.Infrastructure;

/// <summary>
///     This class is used for fluent validation
/// </summary>
public class ModelStateValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var factory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

        var problem = factory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
        context.Result = new ObjectResult(problem) { StatusCode = StatusCodes.Status400BadRequest };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // no action needed here
    }
}