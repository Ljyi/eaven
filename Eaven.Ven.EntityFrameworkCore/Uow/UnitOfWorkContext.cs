using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Uow
{
    /// <summary>
    ///单元操作实现基类
    /// </summary>
    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        public UnitOfWorkContext(DbContext dbContext)
        {
                LazyContext = new Lazy<DbContext>(() => dbContext);
        }
        /// <summary>
        ///获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        protected DbContext Context
        {
            get
            {
                return LazyContext.Value;
            }
        }
        protected Lazy<DbContext> LazyContext { get; set; }

        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {
                int result = Context.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                //更新异常做重试机制
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg = "异常信息：" + msg + "InnerException:" + e.InnerException.Message.ToString();
                }
                throw e;
            }
        }
        /// <summary>
        ///为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : EntityModel
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        ///注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            try
            {
                EntityState state = Context.Entry(entity).State;
                if (state == EntityState.Detached)
                {
                    Context.Entry(entity).State = EntityState.Added;
                }
                IsCommitted = false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }

        /// <summary>
        ///批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel
        {
            try
            {
                //   Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                //  Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            IsCommitted = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        public void RegisterModified<TEntity>(TEntity entity, List<string> list) where TEntity : EntityModel
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    Context.Entry(entity).Property(item).IsModified = true;
                }
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            IsCommitted = false;
        }
        /// <summary>
        /// 根据字段跟新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        public void RegisterModified<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : EntityModel
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    Context.Entry(entity).Property(property).IsModified = true;
                }
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            IsCommitted = false;
        }
        /// <summary>
        ///批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterModified<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel
        {
            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterModified(entity);
                }
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
        /// <summary>
        ///注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        ///批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityModel
        {
            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int ExecuteBySQL(string sql, params object[] parameters)
        {
            var q = Context.Database.ExecuteSqlRaw(sql, parameters);
            return q;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetListBySQL<TEntity>(string sql, params object[] parameters)
        {
            //IEnumerable<TEntity> list = new IEnumerable<TEntity>();
            // Context.Set<TEntity>().FromSql(sql);
            return null;
        }
        /// <summary>
        /// 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync<TEntity>(TEntity entity, EntityState entityState) where TEntity : EntityModel
        {
            Context.Entry<TEntity>(entity).State = entityState;
            return await Context.SaveChangesAsync();
        }

        public int ExecuteBySQL(string sql, DbParameter[] para)
        {
            var q = Context.Database.ExecuteSqlRaw(sql, para);
            return q;
        }
    }

}
