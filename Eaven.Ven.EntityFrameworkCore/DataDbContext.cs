using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore
{
    public class DataDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DataDbContext(WriteAndRead writeRead)
        {

        }
    }
}
