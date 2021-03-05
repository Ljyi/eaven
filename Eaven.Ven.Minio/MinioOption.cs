using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Minio
{
    /// <summary>
    /// 
    /// </summary>
    public class MinioOption
    {
        /// <summary>
        /// 对象存储服务的URL
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// Access key是唯一标识你的账户的用户ID。
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// Secret key是你账户的密码。
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// true代表使用HTTPS
        /// </summary>
        public bool Secure { get; set; }
    }
}
