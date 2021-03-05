using Eaven.Ven.EntityFrameworkCore.MySQL.Extensions;
using Eaven.Ven.EntityFrameworkCore.Repository;
using Eaven.Ven.EntityFrameworkCore.Uow;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.MySQL
{
    public class BaseRepository<TDbContext, TEntity> : IEfRepository<TEntity> where TDbContext : DataDbContext where TEntity : EntityModel
    {
        WriteAndRead AndRead;
        public BaseRepository(WriteAndRead writeAndRead = WriteAndRead.Write)
        {
            AndRead = writeAndRead;
        }
        public IUnitOfWorkContext UnitOfWork { get; set; }
        /// <summary>
        ///获取或设置的数据仓储上下文
        /// </summary>
        protected IUnitOfWorkContext DbContext
        {
            get
            {
                if (UnitOfWork == null)
                {
                    UnitOfWork = new UnitOfWorkContext(AndRead);
                }
                if (UnitOfWork is IUnitOfWorkContext)
                {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                throw new Exception(string.Format("数据仓储上下文对象类型不正确，应为IRepositoryContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }
        ////定义数据访问上下文对象
        //private readonly Lazy<DataDbContext> _dbMaster = new Lazy<DataDbContext>(() => DbContextFactory.CreateWriteDbContext());
        //private readonly Lazy<DataDbContext> _dbSlave = new Lazy<DataDbContext>(() => DbContextFactory.CreateReadDbContext());

        ///// <summary>
        ///// 主库,写操作
        ///// </summary>
        //protected DataDbContext DbMaster => _dbMaster.Value;

        ///// <summary>
        ///// 从库,读操作
        ///// </summary>
        //protected DataDbContext DbSlave => _dbSlave.Value;


        #region 验证是否存在
        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在:true 不存在：false</returns>
        public virtual bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            var entry = DbContext.Set<TEntity>().Where(predicate);
            return (entry.FirstOrDefault() != null);
        }
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在:true 不存在：false</returns>
        public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entry = DbContext.Set<TEntity>().Where(predicate);
            return await Task.Run(() => entry.FirstOrDefault() != null);
        }

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        public virtual bool IsExist(string sql, params DbParameter[] para)
        {
            return DbContext.ExecuteBySQL(sql, para) > 0;
        }
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => DbContext.ExecuteBySQL(sql, para) > 0);
        }
        #endregion


        #region 查询
        /// <summary>
        ///查找指定主键的实体记录
        /// </summary>
        /// <param name="id"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public TEntity Find(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// 查找指定主键的实体记录(异步)
        /// </summary>
        /// <param name="id">指定主键</param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().AsNoTracking().SingleOrDefault(predicate);
        }
        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => DbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate));
        }
        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        public IQueryable<TEntity> Entities
        {
            get { return DbContext.Set<TEntity>(); }
        }
        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression, bool isNoTrack = true)
        {
            if (isNoTrack)
            {
                return DbContext.Set<TEntity>().AsExpandable().Where(expression).AsNoTracking();
            }
            else
            {
                var Queryable = DbContext.Set<TEntity>().AsExpandable().Where(expression);
                return Queryable;
            }
        }
        #endregion

        #region 添加
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Insert(TEntity entity, bool IsCommit = true)
        {
            DbContext.RegisterNew(entity);
            if (IsCommit)
            {
                return DbContext.Commit() > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 增加一条记录(异步方式)
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(TEntity entity, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterNew(entity);
                if (IsCommit)
                {
                    return await Task.Run(() => DbContext.Commit() > 0);
                }
                else
                {
                    return await Task.Run(() => false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Insert(IEnumerable<TEntity> entities, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterNew(entities);
                return IsCommit ? DbContext.Commit() : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量插入实体记录集合
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterNew(entityList);
                if (IsCommit)
                {
                    return await Task.Run(() => DbContext.Commit() > 0);
                }
                else
                {
                    return await Task.Run(() => false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新 
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Update(TEntity entity, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterNew(entity);
                if (IsCommit)
                {
                    return DbContext.Commit() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual bool Update(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterModified(entityList);
                if (IsCommit)
                {
                    return DbContext.Commit() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(TEntity entity, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterModified(entity);
                if (IsCommit)
                {
                    return await Task.Run(() => DbContext.Commit() > 0);
                }
                else
                {
                    return await Task.Run(() => false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 批量更新实体（异步方式）
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            try
            {
                DbContext.RegisterModified(entityList);
                if (IsCommit)
                {
                    return await Task.Run(() => DbContext.Commit() > 0);
                }
                else
                {
                    return await Task.Run(() => false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public int Delete(int id, bool IsCommit = true)
        {
            var entity = DbContext.Set<TEntity>().Find(id);
            DbContext.RegisterDeleted(entity);
            return IsCommit ? DbContext.Commit() : 0;
        }
        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        //public int Delete(int[] id, bool IsCommit = true)
        //{
        //    var entityList = DbContext.Set<TEntity>().Where(p => id.Contains(p.Id));
        //    foreach (var item in entityList)
        //    {
        //        DbContext.RegisterDeleted(item);
        //    }
        //    return IsCommit ? DbContext.Commit() : 0;
        //}
        /// <summary>
        ///删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="IsCommit"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(IEnumerable<TEntity> entities, bool IsCommit = true)
        {
            DbContext.RegisterDeleted(entities);
            return IsCommit ? DbContext.Commit() : 0;
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Delete(TEntity entity, bool IsCommit = true)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }
                else
                {
                    DbContext.RegisterDeleted(entity);
                    if (IsCommit)
                    {
                        return DbContext.Commit() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool DeleteList(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            try
            {
                if (entityList == null)
                {
                    return false;
                }
                else
                {
                    DbContext.RegisterDeleted(entityList);
                    if (IsCommit)
                    {
                        return DbContext.Commit() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(TEntity entity, bool IsCommit = true)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (entity == null)
                    {
                        return false;
                    }
                    else
                    {
                        DbContext.RegisterDeleted(entity);
                        if (IsCommit)
                        {
                            return DbContext.Commit() > 0;
                        }
                        else
                        {
                            return false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> DeleteListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (entityList == null || entityList.Count() < 1)
                    {
                        return false;
                    }
                    else
                    {
                        DbContext.RegisterDeleted(entityList);
                        if (IsCommit)
                        {
                            return DbContext.Commit() > 0;
                        }
                        else
                        {
                            return false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            IEnumerable<TEntity> entities = DbContext.Set<TEntity>().Where(predicate);
            DbContext.RegisterDeleted(entities);
            if (IsCommit)
            {
                return DbContext.Commit() > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            IEnumerable<TEntity> entities = DbContext.Set<TEntity>().Where(predicate);
            DbContext.RegisterDeleted(entities);
            return await Task.Run(() =>
            {
                if (entities == null || entities.Count() < 1)
                {
                    return false;
                }
                else
                {
                    DbContext.RegisterDeleted(entities);
                    if (IsCommit)
                    {
                        return DbContext.Commit() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            });
        }
        #endregion

        #region 获取多条数据操作
        /// <summary>
        /// Lamda返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null ? DbContext.Set<TEntity>().Where(predicate).AsNoTracking<TEntity>() : DbContext.Set<TEntity>().AsQueryable<TEntity>().AsNoTracking<TEntity>();
        }
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTrack = true)
        {
            return await Task.Run(() =>
            {
                if (isNoTrack)
                {
                    if (predicate != null)
                    {
                        return DbContext.Set<TEntity>().Where(predicate).AsNoTracking();
                    }
                    else
                    {
                        return DbContext.Set<TEntity>().AsQueryable<TEntity>().AsNoTracking();
                    }
                }
                else
                {
                    if (predicate != null)
                    {
                        return DbContext.Set<TEntity>().Where(predicate);
                    }
                    else
                    {
                        return DbContext.Set<TEntity>().AsQueryable<TEntity>();
                    }
                }
            });
        }

        /// <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> entitieList = new List<TEntity>();
            if (predicate != null)
            {
                entitieList = DbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToList();
            }
            else
            {
                entitieList = DbContext.Set<TEntity>().AsQueryable<TEntity>().AsNoTracking().ToList();
            }
            return entitieList;
        }
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null ? await Task.Run(() => DbContext.Set<TEntity>().Where(predicate).ToList()) :
                await Task.Run(() => DbContext.Set<TEntity>().AsQueryable<TEntity>().ToList());
        }

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAllBySql(string sql, params DbParameter[] para)
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql, para);
        }
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAllBySql(string sql)
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql);
        }
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> GetAllBySqlAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => DbContext.Set<TEntity>().FromSqlRaw(sql, para));
        }
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> GetAllBySqlAsync(string sql)
        {
            return await Task.Run(() => DbContext.Set<TEntity>().FromSqlRaw(sql));
        }
        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual List<TEntity> GetAllListBySql(string sql, params DbParameter[] para)
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql, para).Cast<TEntity>().ToList();
        }
        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual List<TEntity> GetAllListBySql(string sql)
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql).Cast<TEntity>().ToList();
        }
        /// <summary>
        /// T-Sql方式：返回List<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllListBySqlAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => DbContext.Set<TEntity>().FromSqlRaw(sql, para).Cast<TEntity>().ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOrderBy"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="selector"></param>
        /// <param name="IsAsc"></param>
        /// <returns></returns>
        public List<TResult> QueryEntity<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual List<TResult> QueryEntity<T, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where T : class
            where TResult : class
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.Cast<TResult>().AsNoTracking().ToList();
            }
            return query.Select(selector).AsNoTracking().ToList();
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual async Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where T : class
            where TResult : class
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return await Task.Run(() => query.Cast<TResult>().AsNoTracking().ToList());
            }
            return await Task.Run(() => query.Select(selector).AsNoTracking().ToList());
        }

        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <typeparam name="Tkey">泛型</typeparam>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>IQueryable 泛型集合</returns>
        public IQueryable<TEntity> LoadPageItems(int pageSize, int pageIndex, out int total, Expression<Func<TEntity, bool>> whereLambda, string orderby, bool isAsc)
        {
            total = DbContext.Set<TEntity>().Where(whereLambda).Count();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = "Id";//默认以Id排序
            }
            var temp = DbContext.Set<TEntity>().Where(whereLambda).ToList()
                         .OrderByBatch<TEntity>(orderby, isAsc)
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize);
            return temp.AsQueryable();

        }
        #endregion
    }
}
