using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Socket.WebSocket
{
    /// <summary>
    /// 核心类实现的配置所需
    /// </summary>
    public class WebSocketClientOptions
    {
        /// <summary>
        /// 负载的服务端
        /// </summary>
        public string[] Servers { get; set; }
        /// <summary>
        /// websocket请求的路径，默认值：/ws
        /// </summary>
        public string PathMatch { get; set; } = "/ws";
    }
}
