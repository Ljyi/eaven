using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Repository
{
    public interface IEfRepository<TEntity> : IRepository<TEntity> where TEntity : EntityModel
    {
        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        bool IsExist(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        bool IsExist(string sql, params DbParameter[] para);

        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        Task<bool> IsExistAsync(string sql, params DbParameter[] para);
        #endregion

        #region 单模型 CRUD 操作
        #region 查询
        /// <summary>
        /// 根据主键Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);
        /// <summary>
        /// 根据主键Id查询（异步）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(int id);
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression, bool isNoTrack = true);
        #endregion

        #region 添加
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Insert(TEntity entity, bool IsCommit = true);

        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> InsertAsync(TEntity entity, bool IsCommit = true);

        /// <summary>
        ///批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="IsCommit"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool IsCommit = true);

        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> InsertAsync(IEnumerable<TEntity> entityList, bool IsCommit = true);
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Update(TEntity entity, bool IsCommit = true);
        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        bool Update(IEnumerable<TEntity> entityList, bool IsCommit = true);
        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, bool IsCommit = true);

        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> UpdateListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true);

        #endregion

        #region 删除
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">实体记录编号</param>
        /// <param name="IsCommit">是否执行保存</param>
        /// <returns></returns>
        int Delete(int id, bool IsCommit = true);
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool Delete(TEntity entity, bool IsCommit = true);

        /// <summary>
        ///删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="IsCommit"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        //int Delete(int[] id, bool IsCommit = true);

        /// <summary>
        /// 删除一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity, bool IsCommit = true);

        /// <summary>
        ///删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="IsCommit"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool IsCommit = true);

        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool DeleteList(IEnumerable<TEntity> entityList, bool IsCommit = true);

        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="entityList">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        Task<bool> DeleteListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true);

        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true);

        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true);


        #endregion
        #endregion

        #region 获取多条数据操作

        /// <summary>
        /// 返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTrack = true);

        // <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllBySql(string sql, params DbParameter[] para);

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllBySql(string sql);
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllBySqlAsync(string sql, params DbParameter[] para);

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllBySqlAsync(string sql);

        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        List<TEntity> GetAllListBySql(string sql, params DbParameter[] para);

        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        List<TEntity> GetAllListBySql(string sql);
        /// <summary>
        /// T-Sql方式：返回List<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListBySqlAsync(string sql, params DbParameter[] para);

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
        List<TResult> QueryEntity<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class;

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
        Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class;
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<TEntity> LoadPageItems(int pageSize, int pageIndex, out int total, Expression<Func<TEntity, bool>> whereLambda, string orderby, bool isAsc);
        #endregion
    }
}
