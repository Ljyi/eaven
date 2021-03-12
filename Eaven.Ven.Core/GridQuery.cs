using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eaven.Ven.Core
{
    public class GridQuery
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 限制条数
        /// </summary>
        [Range(5, 500)]
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 排序列名
        /// </summary>
     //   [JsonIgnoreAttribute]
        public string SortName { get; set; } = "Id";

        /// <summary>
        /// 排序方式(asc/desc)
        /// </summary>
      //  [JsonIgnoreAttribute]
        public string SortOrder { get; set; }
        /// <summary>
        /// 不排序
        /// </summary>
        /// <remarks>
        /// </remarks>
        //[JsonIgnoreAttribute]
        public bool NotSort { get; set; }
        /// <summary>
        ///构造方法
        /// </summary>
        public GridQuery()
        {
            this.PageSize = 5;// Defaults.PageSize;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        public GridQuery(int pageSize, string sortName, string sortOrder)
        {
            this.PageSize = pageSize;
            this.SortName = sortName;
            this.SortOrder = sortOrder;
        }
        /// <summary>
        ///初始化
        /// </summary>
        public void GridQueryInit<T>(IQueryable<T> entitys)
        {
            if (entitys != null)
            {
                TotalCount = entitys.Count();
                if (TotalCount > 0)
                {
                    if (PageIndex < 1)
                    {
                        PageIndex = 0;
                    }
                    else
                    {
                        PageIndex = PageIndex - 1;
                    }
                }
            }
        }
    }
}
