using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace ProductClientHub.API.Filters;
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProductClientHubException productClientHubException)
        {
            context.HttpContext.Response.StatusCode = (int)productClientHubException.GetHttpStatusCode();

            context.Result = new ObjectResult(new ResponseErrorMessagesJson(productClientHubException.GetErrors()));
        }
        else
        {
            ThrowUnkownError(context);
        }
    }
    private void ThrowUnkownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorMessagesJson("ERRO DESCONHECIDO"));
    }

}

