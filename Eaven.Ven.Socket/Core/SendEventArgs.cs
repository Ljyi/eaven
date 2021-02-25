using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Socket.Core
{
    public class SendEventArgs : EventArgs
    {
        /// <summary>
        /// 发送者的客户端id
        /// </summary>
        public Guid SenderClientId { get; }
        /// <summary>
        /// 接收者的客户端id
        /// </summary>
        public List<Guid> ReceiveClientId { get; } = new List<Guid>();
        /// <summary>
        /// imServer 服务器节点
        /// </summary>
        public string Server { get; }
        /// <summary>
        /// 消息
        /// </summary>
        public object Message { get; }
        /// <summary>
        /// 是否回执
        /// </summary>
        public bool Receipt { get; }

        internal SendEventArgs(string server, Guid senderClientId, object message, bool receipt = false)
        {
            this.Server = server;
            this.SenderClientId = senderClientId;
            this.Message = message;
            this.Receipt = receipt;
        }
    }
}
