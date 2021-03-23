using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore
{
    //public class DataDbContext : DbContext
    //{
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="options"></param>
    //    public DataDbContext(WriteAndRead writeRead)
    //    {

    //    }
    //}
    public class DataDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
            string msg = "链接字符串";
        }
        public DataDbContext(string sql)
        {

        }
        public DataDbContext(WriteAndRead writeRead)
        {

        }
    }
}
