using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Elasticsearch.Model
{
    /// <summary>
    /// 基类
    /// </summary>
    [ElasticsearchType(IdProperty = "Id")]
    public class EsBaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Keyword]
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    /// 日志基类
    /// </summary>
    [ElasticsearchType(IdProperty = "Id")]
    public class EsLogBaseModle : EsBaseModel
    {
        /// <summary>
        /// 检索Id（table）
        /// </summary>
        [Keyword]
        public string LogIndex { get; set; }
        /// <summary>
        /// Json数据
        /// </summary>
        [Text]
        public string Json { get; set; }
    }
    /// <summary>
    /// 搜索基类
    /// </summary>
    [ElasticsearchType(IdProperty = "Id")]
    public class EsSearchBaseModle : EsBaseModel
    {
        /// <summary>
        /// 检索Id（table）
        /// </summary>
        [Keyword]
        public string Index { get; set; }
        /// <summary>
        /// Json数据
        /// </summary>
        [Text(Name = "Json")]//, Index = true, Analyzer = "ik_max_word")
        public string Json { get; set; }
    }
}
