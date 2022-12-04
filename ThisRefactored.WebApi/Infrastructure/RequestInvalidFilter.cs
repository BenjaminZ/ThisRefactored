using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ThisRefactored.Application.Exceptions;

namespace ThisRefactored.WebApi.Infrastructure;

/// <summary>
///     This filter will catch all other exceptions and return a 500 response
/// </summary>
public class RequestInvalidFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // no action needed here
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.ExceptionHandled || context.Exception is not StatusCodeException ex)
        {
            return;
        }

        var factory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

        context.ExceptionHandled = true;
        var problem = factory.CreateProblemDetails(context.HttpContext);
        problem.Status = ex.StatusCode;
        problem.Title = ex.Message;
        context.Result = new ObjectResult(problem) { StatusCode = ex.StatusCode };
    }
}