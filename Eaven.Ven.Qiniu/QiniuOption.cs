using System;

namespace Eaven.Ven.Qiniu
{
    public class QiniuOption
    {
        /// <summary>
        /// AccessKey
        /// </summary>
        private static string AccessKey { get; set; }
        /// <summary>
        /// SecretKey
        /// </summary>
        private static string SecretKey { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        private static string Bucket { get; set; }
        /// <summary>
        /// 文件返回Url
        /// </summary>
        private static string Url { get; set; }
    }
}
