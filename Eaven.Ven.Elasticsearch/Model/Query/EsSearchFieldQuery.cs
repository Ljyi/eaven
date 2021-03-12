using Eaven.Ven.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Elasticsearch.Model.Query
{
    /// <summary>
    /// 查询
    /// </summary>
    public class EsSearchFieldQuery
    {
        /// <summary>
        /// 查询字段
        /// 多个用“,”分割
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }
    }
    /// <summary>
    /// 查询
    /// </summary>
    public class EsSearchFieldQueryPage : GridQuery
    {
        public EsSearchFieldQueryPage()
        {
            SearchFieldQuery = new List<EsSearchFieldQuery>();
        }
        public List<EsSearchFieldQuery> SearchFieldQuery { get; set; }
    }
}
