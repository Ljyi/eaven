using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Host.Filter
{
    /// <summary>
    /// 异常拦截器 拦截 StatusCode>=500 的异常
    /// </summary>
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            //var status = 500;
            //var exception = context.Exception;
            //var eventId = new EventId(exception.HResult);
            //var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
            //var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //var hostAndPort = context.HttpContext.Request.Host.HasValue ? context.HttpContext.Request.Host.Value : string.Empty;
            //var requestUrl = string.Concat(hostAndPort, context.HttpContext.Request.Path);
            //var type = string.Concat("https://httpstatuses.com/", status);

            //string title;
            //string detial;
            //if (exception is IAdncException)
            //{
            //    title = "参数错误";
            //    detial = exception.Message;
            //}
            //else
            //{
            //    title = _env.IsDevelopment() ? exception.Message : $"系统异常";
            //    detial = _env.IsDevelopment() ? ExceptionHelper.GetExceptionDetail(exception) : $"系统异常,请联系管理员({eventId})";
            //    _logger.LogError(eventId, exception, exception.Message, requestUrl, userContext.Id);
            //}

            //var problemDetails = new ProblemDetails
            //{
            //    Title = title
            //    ,
            //    Detail = detial
            //    ,
            //    Type = type
            //    ,
            //    Status = status
            //    ,
            //    Instance = requestUrl
            //};

            //   context.Result = new ObjectResult(problemDetails) { StatusCode = status };
            context.ExceptionHandled = true;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }


    }
}
