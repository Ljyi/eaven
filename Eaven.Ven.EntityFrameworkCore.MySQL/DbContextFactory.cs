using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.MySQL
{
    public class DbContextFactory : IDbContextFactory
    {
        private static IConfiguration _configuration;
        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public DbContext CreateDbContext(WriteAndRead dbType)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 获取DbContext的Options
        /// </summary>
        /// <param name="writeRead"></param>
        /// <returns></returns>
        public static DbContextOptions<DataDbContext> GetOptions(WriteAndRead writeRead)
        {
            string masterConnectionString = _configuration.GetConnectionString("MySql:Write");
            //随机选择读数据库节点
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            if (writeRead == WriteAndRead.Read)
            {
                string slaveConnectionString = _configuration.GetConnectionString("MySql:Read");
                optionsBuilder.UseMySQL(slaveConnectionString);
                //optionsBuilder.UseLazyLoadingProxies();//启用延迟加载
            }
            else
            {
                optionsBuilder.UseMySQL(masterConnectionString);
            }
            return optionsBuilder.Options;
        }
        /// <summary>
        /// 创建ReadDbContext实例
        /// </summary>
        /// <returns></returns>
        public static DataDbContext CreateReadDbContext()
        {
            //先从线程获取实例，保证线程安全
            DataDbContext dbContext = (DataDbContext)CallContext.GetData(WriteAndRead.Read);
            if (dbContext == null)
            {
                dbContext = new DataDbContext(WriteAndRead.Read);
                CallContext.SetData(WriteAndRead.Read, dbContext);
            }
            return dbContext;
        }

        /// <summary>
        /// 创建WriteDbContext实例
        /// </summary>
        /// <returns></returns>
        public static DataDbContext CreateWriteDbContext()
        {
            //先从线程获取实例，保证线程安全
            DataDbContext dbContext = (DataDbContext)CallContext.GetData(WriteAndRead.Write);
            if (dbContext == null)
            {
                dbContext = new DataDbContext(WriteAndRead.Write);
                CallContext.SetData(WriteAndRead.Write, dbContext);
            }
            return dbContext;
        }
    }
}
