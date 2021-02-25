using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Socket.WebSocket.Extensions
{
    public static class WebSocketServiceCollection
    {
        /// <summary>
        /// 启用 WebSocketServer 服务端
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder app, WebSocketServerOptions options)
        {
            app.Map(options.PathMatch, appcur =>
            {
                var imserv = new WebSocketServer(options);
                if (options.IsWebSocket)
                {
                    appcur.UseWebSockets();
                }
                appcur.Use((ctx, next) => imserv.Acceptor(ctx, next));
            });
            return app;
        }
    }
}
