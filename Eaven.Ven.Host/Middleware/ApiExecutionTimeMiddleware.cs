using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Host.Middleware
{
    /// <summary>
    /// 接口时间记录中间件
    /// </summary>
    public class ApiExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;//下一个中间件
        private readonly ILogger _logger;
        // private readonly IHttpContextAccessor httpContextAccessor;
        Stopwatch stopwatch;
        public ApiExecutionTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            this._next = next;
            _logger = loggerFactory.CreateLogger<ApiExecutionTimeMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();//在下一个中间价处理前，启动计时器
                await _next.Invoke(context);
                stopwatch.Stop();//所有的中间件处理完后，停止秒表。
                var userId = context.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Sid)?.Value;
                var eq = context.Request.Headers["User-Agent"].ToString();
                var ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                _logger.LogInformation($@"设备：{eq},用户ID：{userId},IP：{ip} 接口：{context.Request.Path}耗时{stopwatch.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ApiExecutionTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseCalculateExecutionTime(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<ApiExecutionTimeMiddleware>();
        }
    }
}
