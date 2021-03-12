using Eaven.Ven.Core;
using Eaven.Ven.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore
{
    /// <summary>
    /// 基础服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : EntityModel
    {

        public IEfRepository<TEntity> baseRepository;
        public BaseServices(IEfRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<TEntity> Entities
        {
            get { return baseRepository.Entities; }
        }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(int id)
        {
            return baseRepository.Find(id);
        }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(string id)
        {
            return baseRepository.Find(int.Parse(id.ToString()));
        }
        /// <summary>
        /// 根据Id查询（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<TEntity> FindById(object Id)
        {
            return Task.Run(() =>
            {
                return baseRepository.Find(int.Parse(Id.ToString()));
            });
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return baseRepository.Get(predicate);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return baseRepository.GetAll(predicate);
        }
        /// <summary>
        /// 根据条件查询（异步）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return baseRepository.GetAllAsync(predicate);
            });
        }
        /// <summary>
        /// 根据条件查询（sql）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllBySql(string sql, params DbParameter[] para)
        {
            return baseRepository.GetAllBySql(sql, para);
        }
        /// <summary>
        /// 根据条件查询（异步 sql）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> GetAllBySqlAsync(string sql, params DbParameter[] para)
        {
            return Task.Run(() =>
            {
                return baseRepository.GetAllBySql(sql, para);
            });
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return baseRepository.GetAllList(predicate);
        }
        /// <summary>
        ///  根据条件查询 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return baseRepository.GetAllListAsync(predicate);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<TEntity> GetAllListBySql(string sql, params DbParameter[] para)
        {
            return baseRepository.GetAllListBySql(sql, para);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllListBySqlAsync(string sql, params DbParameter[] para)
        {
            return Task.Run(() =>
            {
                return baseRepository.GetAllListBySqlAsync(sql, para);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return baseRepository.GetAsync(predicate);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isNoTrack"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression, bool isNoTrack = true)
        {
            return baseRepository.GetEntities(expression, isNoTrack);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool Insert(TEntity entity, bool IsCommit = true)
        {
            return baseRepository.Insert(entity, IsCommit);
        }
        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> entities, bool IsCommit = true)
        {
            return baseRepository.Insert(entities, IsCommit);
        }
        /// <summary>
        /// 添加单个实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> InsertAsync(TEntity entity, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.Insert(entity, IsCommit);
            });
        }
        /// <summary>
        /// 添加多个实体（异步）
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> InsertAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.InsertAsync(entityList, IsCommit);
            });
        }
        /// <summary>
        /// 是否存在（条件）
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<TEntity, bool>> whereExpression)
        {
            return baseRepository.IsExist(whereExpression);
        }
        /// <summary>
        /// 是否存在（sql）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool IsExist(string sql, params DbParameter[] para)
        {
            return baseRepository.IsExist(sql, para);
        }
        /// <summary>
        /// 是否存在（条件）异步
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Task.Run(() =>
            {
                return baseRepository.IsExistAsync(whereExpression);
            });
        }
        /// <summary>
        /// 是否存在（sql 条件）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public Task<bool> IsExistAsync(string sql, params DbParameter[] para)
        {
            return Task.Run(() =>
            {
                return baseRepository.IsExistAsync(sql, para);
            });
        }

        /// <summary>
        /// 单个实体更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool Update(TEntity entity, bool IsCommit = true)
        {
            return baseRepository.Update(entity, IsCommit);
        }
        /// <summary>
        /// 单个实体更新（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(TEntity entity, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.UpdateAsync(entity, IsCommit);
            });
        }
        /// <summary>
        /// 多个实体更新 
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> UpdateListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.UpdateListAsync(entityList, IsCommit);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public int Delete(int id, bool IsCommit = true)
        {
            return baseRepository.Delete(id, IsCommit);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity, bool IsCommit = true)
        {
            return baseRepository.Delete(entity, IsCommit);
        }
        /// <summary>
        /// 删除多个实体 根据Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public int Delete(int[] ids, bool IsCommit = true)
        {
            int rowNum = 0;
            foreach (var id in ids)
            {
                if (baseRepository.Delete(id, IsCommit) > 0)
                {
                    rowNum++;
                }
            }
            return rowNum;
        }
        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<TEntity> entities, bool IsCommit = true)
        {
            return baseRepository.Delete(entities, IsCommit);
        }
        /// <summary>
        ///  删除多个（条件）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            return baseRepository.Delete(predicate, IsCommit);
        }
        /// <summary>
        /// 删除单个（异步 ）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(TEntity entity, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.DeleteAsync(entity, IsCommit);
            });
        }
        /// <summary>
        /// 删除单个（异步 ）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.DeleteAsync(predicate, IsCommit);
            });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public bool DeleteList(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            return baseRepository.DeleteList(entityList, IsCommit);
        }
        /// <summary>
        /// 批量删除（异步 ）
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public Task<bool> DeleteListAsync(IEnumerable<TEntity> entityList, bool IsCommit = true)
        {
            return Task.Run(() =>
            {
                return baseRepository.DeleteListAsync(entityList, IsCommit);
            });
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
            return baseRepository.QueryEntity(where, orderby, selector, IsAsc);
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
        public Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class
        {
            return Task.Run(() =>
            {
                return baseRepository.QueryEntity(where, orderby, selector, IsAsc);
            });
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<T> GetTablePagedList<T>(IQueryable<T> entitys, GridQuery gp)
        {
            gp.GridQueryInit(entitys);
            if (!gp.NotSort)
            {
                try
                {
                    if (string.IsNullOrEmpty(gp.SortName))
                    {
                        entitys = entitys.OrderBy("Id");
                    }
                    else
                    {
                        try
                        {
                            entitys = entitys.OrderBy(gp.SortName + " " + gp.SortOrder); //排序
                        }
                        catch (Exception)
                        {
                            entitys = entitys.OrderBy("Id");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            entitys = entitys.Skip(gp.PageIndex * gp.PageSize).Take(gp.PageSize); //分页
            return entitys.ToList();
        }
    }
}
