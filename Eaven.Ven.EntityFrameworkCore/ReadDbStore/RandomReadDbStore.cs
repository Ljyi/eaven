using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.ReadDbStore
{
    /// <summary>
    /// 随机策略
    /// </summary>
    public class RandomReadDbStore : IReadDbStore
    {
        //所有读库类型
        public static List<Type> DbTypes;

        static RandomReadDbStore()
        {
            LoadDbs();
        }

        //加载所有的读库类型
        static void LoadDbs()
        {
            DbTypes = new List<Type>();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                //if (type.BaseType == typeof(BaseReadDbContext))
                //{
                //    DbTypes.Add(type);
                //}
            }
        }
        public DbContext GetDbContext()
        {
            int randomIndex = new Random().Next(0, DbTypes.Count);
            var dbType = DbTypes[randomIndex];
            var dbContext = Activator.CreateInstance(dbType) as DbContext;
            return dbContext;
        }
    }
}
