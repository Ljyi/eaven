using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Socket.WebSocket
{
    /// <summary>
    /// 服务端
    /// </summary>
    public class WebSocketServerClient
    {
        public System.Net.WebSockets.WebSocket socket;
        public Guid clientId;

        public WebSocketServerClient(System.Net.WebSockets.WebSocket socket, Guid clientId)
        {
            this.socket = socket;
            this.clientId = clientId;
        }
    }
}
