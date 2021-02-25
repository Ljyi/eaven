using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.ReadDbStore
{
    /// <summary>
    /// 从数据库获取策略接口
    /// </summary>
    interface IReadDbStore
    {
        /// <summary>
        /// 获取读库
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
    }
}
