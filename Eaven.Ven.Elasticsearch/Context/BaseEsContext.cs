using Eaven.Ven.Core.Extension;
using Eaven.Ven.Elasticsearch.Extensions;
using Eaven.Ven.Elasticsearch.Model;
using Eaven.Ven.Elasticsearch.Model.Query;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Elasticsearch.Context
{
    /// <summary>
    /// es操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEsContext<T> : IBaseEsContext where T : EsBaseModel
    {
        protected IEsClientProvider _EsClientProvider;
        public abstract string IndexName { get; }
        public static ElasticClient client { get; set; }
        public static ElasticLowLevelClient lowlevelClient { get; set; }
        public BaseEsContext(IEsClientProvider provider)
        {
            if (client == null || lowlevelClient == null)
            {
                _EsClientProvider = provider;
                client = _EsClientProvider.GetClient(IndexName);
                if (!client.Indices.Exists(IndexName).Exists)
                {
                    client.CreateIndex<T>(IndexName);
                }
                lowlevelClient = _EsClientProvider.GetElasticLowLevelClient();
            }
        }
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert(T t)
        {
            try
            {
                var response = client.IndexDocument(t);
                return response.IsValid;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine(msg);
                return false;
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool InsertMany(List<T> tList)
        {
            try
            {
                var response = client.Bulk(p => p.Index(IndexName).IndexMany(tList));
                return response.IsValid;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine(msg);
                return false;
            }
        }

        /// <summary>
        /// 批量添加(异步)
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool InsertManyAsync(List<T> tList)
        {
            try
            {
                var response = client.BulkAsync(p => p.Index(IndexName).IndexMany(tList));
                return response.Result.IsValid;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine(msg);
                return false;
            }
        }

        #endregion

        #region 编辑
        ///// <summary>
        ///// 更新部分字段
        ///// </summary>
        ///// <returns></returns>
        //public bool UpdatePart(UpdateRequest<T, object> request)
        //{
        //    var response = client.Update(request);
        //    return response.IsValid;
        //}
        ///// <summary>
        ///// 更新部分字段
        ///// </summary>
        ///// <returns></returns>
        //public bool UpdatePartAsync(UpdateRequest<T, object> request)
        //{
        //    var response = client.UpdateAsync(request);
        //    return response.Result.IsValid;
        //}
        /// <summary>
        /// 更新全部字段
        /// </summary>
        /// <returns></returns>
        public bool Update(T t)
        {
            DocumentPath<T> updatePath = new DocumentPath<T>(t.Id);
            var response = client.Update(updatePath, (p) => p.Doc(t));
            return response.IsValid;
        }
        /// <summary>
        /// 更新全部字段
        /// </summary>
        /// <returns></returns>
        public bool UpdateByQuery(UpdateByQueryRequest t)
        {
            var updateByQueryResponse = client.UpdateByQuery(t);
            return updateByQueryResponse.IsValid;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteById(string id)
        {
            var response = client.Delete<T>(id);
            return response.IsValid;
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            var response = client.DeleteByQuery<T>(zw => zw.MatchAll());
            return response.IsValid;
        }
        #endregion

        #region 批量（增删改）  
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public string Bulk(object[] obs)
        {
            try
            {
                var ndexResponse = lowlevelClient.Bulk<StringResponse>(PostData.MultiJson(obs));
                //StringResponse stringResponse = new StringResponse() { };
                //IEnumerable<StringResponse> stringResponses =  ;
                //client.BulkAll<StringResponse>((IBulkAllRequest<StringResponse>)stringResponses);
                return ndexResponse.Body;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine(msg);
                return "";
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsExist(DocumentPath<T> t)
        {
            var response = client.DocumentExists(t);
            return response.Exists;
        }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(string id)
        {
            var search = new SearchDescriptor<T>().Source(p => p.Includes(x => x.Field(id)));
            var response = client.Get<T>(id);
            return response.Source;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            var search = new SearchDescriptor<T>().MatchAll(); //指定查询字段 .Source(p => p.Includes(x => x.Field("Id")));
            var response = client.Search<T>(search);
            return response.Total;
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            var searchDescriptor = new SearchDescriptor<T>();
            searchDescriptor = searchDescriptor.Query(p => p.MatchAll());
            var response = client.Search<T>(searchDescriptor);
            return response.Documents.ToList();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetGridPage(EsSearchFieldQueryPage query)
        {
            var musts = EsSerchExpression.Must<T>();
            if (query.SearchFieldQuery.Count > 0)
            {
                foreach (var item in query.SearchFieldQuery)
                {
                    if (item.Fields.IndexOf(",") < 1)
                    {
                        musts.AddMatch(item.Fields, item.Query);
                    }
                    else
                    {
                        musts.AddMultiMatch(item.Fields.SplitDefault(), item.Query);
                    }
                }
            }
            var result = client.Search<T>(sd =>
                sd.Query(qcd => qcd
                        .Bool(cc => cc
                            .Must(musts)
                        )
                        ).From(query.PageIndex)
                        .Take(query.PageSize)
            );
            var data = result.Documents;
            query.TotalCount = int.Parse(result.Total.ToString());
            var t = result.Hits.ToList();
            return result.Documents.ToList();
        }
        #endregion

        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="word">词</param>
        /// <returns></returns>
        public List<string> EsAnalyze(string word, Analyzer analyzer = Analyzer.standard)
        {
            var analyzeResponse = client.Indices.Analyze(a => a.Analyzer(EnumExtension.GetEnumDesc(analyzer)).Text(word));
            return analyzeResponse.Tokens.Select(zw => zw.Token).ToList();
        }
    }
}
