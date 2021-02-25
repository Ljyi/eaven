using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb
{
    public class MongoDBaseService<T> where T : BaseModel, new()
    {
        private readonly IMongoCollection<T> mongoDb;   //数据表操作对象
        protected IMogoDBContext DBContext { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MongoDBaseService(IMogoDBContext mogoDBContext, string tbName)
        {
            DBContext = mogoDBContext;
            //获取对特定数据表集合中的数据的访问
            mongoDb = DBContext.Database.GetCollection<T>(tbName);
        }

        #region 添加

        #region 添加一条数据
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <returns></returns>
        public int Add(T t)
        {
            try
            {
                mongoDb.InsertOne(t);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region  异步添加一条数据
        /// <summary>
        /// 异步添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <returns></returns>
        public async Task<int> AddAsync(T t)
        {
            try
            {
                await mongoDb.InsertOneAsync(t);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region  批量插入
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public int InsertMany(List<T> t)
        {
            try
            {
                mongoDb.InsertMany(t);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region  异步批量插入
        /// <summary>
        /// 异步批量插入
        /// </summary>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public async Task<int> InsertManyAsync(List<T> t)
        {
            try
            {
                await mongoDb.InsertManyAsync(t);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 修改

        #region 修改
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t">修改的实体</param>
        /// <returns></returns>
        public UpdateResult Update(T t, string id, bool isObjectId = true)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return mongoDb.UpdateOne(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步修改一条数据
        /// <summary>
        /// 异步修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateAsync(T t, string id, bool isObjectId)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id") continue;
                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await mongoDb.UpdateOneAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 批量修改数据
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public UpdateResult UpdateManay(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return mongoDb.UpdateMany(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步批量修改数据
        /// <summary>
        /// 异步批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateManayAsync(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name)) continue;
                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await mongoDb.UpdateManyAsync(filter, updatefilter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region 删除

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public DeleteResult Delete(string id, bool isObjectId = true)
        {
            try
            {
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                return mongoDb.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 异步删除一条数据
        /// <summary>
        /// 异步删除一条数据
        /// </summary>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteAsync(string id, bool isObjectId = true)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                return await mongoDb.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 删除多条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public DeleteResult DeleteMany(FilterDefinition<T> filter)
        {
            try
            {
                return mongoDb.DeleteMany(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region  异步删除多条数据
        /// <summary>
        /// 异步删除多条数据
        /// </summary>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            try
            {
                return await mongoDb.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #endregion

        #region 查询

        #region  根据id查询一条数据
        /// <summary>
        /// 根据id查询一条数据
        /// </summary>
        /// <param name="id">objectid</param>
        /// <param name="field">要查询的字段，不写时查询全部</param>
        /// <returns></returns>
        public T Find(string id, bool isObjectId = true, string[] field = null)
        {
            try
            {
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return mongoDb.Find(filter).FirstOrDefault<T>();
                }
                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                return mongoDb.Find(filter).Project<T>(projection).FirstOrDefault<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步根据id查询一条数据
        /// <summary>
        /// 异步根据id查询一条数据
        /// </summary>
        /// <param name="id">objectid</param>
        /// <returns></returns>
        public async Task<T> FindAsync(string id, bool isObjectId = true, string[] field = null)
        {
            try
            {
                FilterDefinition<T> filter;
                if (isObjectId)
                {
                    filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                }
                else
                {
                    filter = Builders<T>.Filter.Eq("_id", id);
                }

                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return await mongoDb.Find(filter).FirstOrDefaultAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                return await mongoDb.Find(filter).Project<T>(projection).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询集合
        /// <summary>
        /// 查询集合
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public List<T> GetList(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return mongoDb.Find(filter).ToList();
                    //进行排序
                    return mongoDb.Find(filter).Sort(sort).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null) return mongoDb.Find(filter).Project<T>(projection).ToList();
                //排序查询
                return mongoDb.Find(filter).Sort(sort).Project<T>(projection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步查询集合
        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await mongoDb.Find(filter).ToListAsync();
                    return await mongoDb.Find(filter).Sort(sort).ToListAsync();
                }
                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null) return await mongoDb.Find(filter).Project<T>(projection).ToListAsync();
                //排序查询
                return await mongoDb.Find(filter).Sort(sort).Project<T>(projection).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分页查询集合
        /// <summary>
        /// 分页查询集合
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="count">总条数</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public List<T> GetListByPage(FilterDefinition<T> filter, int pageIndex, int pageSize, out long count, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                count = mongoDb.CountDocuments(filter);
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return mongoDb.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                    //进行排序
                    return mongoDb.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }
                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                //不排序
                if (sort == null) return mongoDb.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                //排序查询
                return mongoDb.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步分页查询集合
        /// <summary>
        /// 异步分页查询集合
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<List<T>> GetListByPageAsync(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await mongoDb.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                    //进行排序
                    return await mongoDb.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }
                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                //不排序
                if (sort == null) return await mongoDb.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

                //排序查询
                return await mongoDb.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据条件获取总数
        /// <summary>
        /// 根据条件获取总数
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public long Count(FilterDefinition<T> filter)
        {
            try
            {
                return mongoDb.CountDocuments(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 异步根据条件获取总数
        /// <summary>
        /// 异步根据条件获取总数
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public async Task<long> CountAsync(FilterDefinition<T> filter)
        {
            try
            {
                return await mongoDb.CountDocumentsAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}
