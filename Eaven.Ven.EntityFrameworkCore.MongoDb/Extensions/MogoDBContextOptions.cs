using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb.Extensions
{
    public class MogoDBContextOptions : IOptions<MogoDBContextOptions>
    {
        /// <summary>The default configured TOptions instance</summary>
        MogoDBContextOptions IOptions<MogoDBContextOptions>.Value => this;
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }
    }
}
