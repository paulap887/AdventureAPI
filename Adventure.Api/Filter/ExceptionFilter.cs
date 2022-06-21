using Adventure.Core.Common;
using Adventure.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

namespace Adventure.Api.Filter
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// On Exception (Handler)
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
             
            if (context != null && context.Exception is DbFailureException)
            {
                statusCode = HttpStatusCode.InternalServerError;
            } 
            if (context != null && context.Exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            if (context != null)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)statusCode;

                context.Result = new JsonResult(new ErrorResponse()
                {
                    Error = context.Exception.Message,
                    Code = statusCode,
                });
            }
        }
    }
}
