using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Socket.WebSocket
{
    /// <summary>
    ///  核心类实现的配置所需
    /// </summary>
    public class WebSocketServerOptions : WebSocketClientOptions
    {
        /// <summary>
        /// 设置服务名称，它应该是 servers 内的一个
        /// </summary>
        public string Server { get; set; }

        public bool IsWebSocket { get; set; }

    }
}
