using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Uow
{
    public interface IUnitOfWorkContext
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        ///为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : EntityModel;

        /// <summary>
        ///注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : EntityModel;

        /// <summary>
        ///批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel;

        /// <summary>
        ///注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TEntity>(TEntity entity) where TEntity : EntityModel;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        void RegisterModified<TEntity>(TEntity entity, List<string> list) where TEntity : EntityModel;
        /// <summary>
        /// 根据字段进行更新
        /// </summary>
        void RegisterModified<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : EntityModel;
        /// <summary>
        ///批量注册多个个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel;

        /// <summary>
        ///注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : EntityModel;

        /// <summary>
        ///批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel;
        /// <summary>
        /// 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync<TEntity>(TEntity entity, EntityState entityState) where TEntity : EntityModel;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetListBySQL<TEntity>(string sql, params object[] parameters);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        int ExecuteBySQL(string sql, DbParameter[] para);
    }
}
