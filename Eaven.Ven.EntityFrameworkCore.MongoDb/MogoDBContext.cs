using Eaven.Ven.EntityFrameworkCore.MongoDb.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb
{
    public class MogoDBContext : IMogoDBContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public MogoDBContext(IOptions<MogoDBContextOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);    //获取链接字符串
            Database = client.GetDatabase(options.Value.DBName);   //数据库名 （不存在自动创建）
        }
        public IMongoDatabase Database
        {
            get; set;
        }
    }
}
