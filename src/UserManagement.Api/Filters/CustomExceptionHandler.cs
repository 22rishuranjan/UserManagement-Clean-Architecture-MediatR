
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.Application.Common.Behaviours;
using UserManagement.Application.Common.Exceptions;

namespace UserManagement.Api.Filters;

public class CustomExceptionHandler : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;

        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
              { typeof(OtpTimeoutException), HandleOtpTimeoutException },
                  { typeof(TooManyRetryException), HandleTooManyRetryException },
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
              
            };
    }

    public override void OnException(ExceptionContext context)
    {
        // Log the exception (you can also log more details here)
        _logger.LogError(context.Exception, "Unhandled exception occurred.");

        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        object detail;

        if(exception.Errors != null)
        {
            detail = new ValidationProblemDetails(exception?.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        else
        {
            detail = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Invalid Operation",
                Detail = exception.Message
            };
        }
        context.Result = new BadRequestObjectResult(detail);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
       
         var  detail = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The specified resource was not found",
                Detail = exception.Message
            };
        
        context.Result = new BadRequestObjectResult(detail);
        context.ExceptionHandled = true;
    }

    private void HandleTooManyRetryException(ExceptionContext context)
    {
        var exception = (TooManyRetryException)context.Exception;

        var detail = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Too many retries",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(detail);
        context.ExceptionHandled = true;
    }

    private void HandleOtpTimeoutException(ExceptionContext context)
    {
        var exception = (OtpTimeoutException)context.Exception;

        var detail = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Otp timeout. Generate new OTP again.",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(detail);
        context.ExceptionHandled = true;
    }

    

}
