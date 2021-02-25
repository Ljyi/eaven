using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Elasticsearch.Context
{
    /// <summary>
    /// 分词
    /// </summary>
    public enum Analyzer
    {
        /// <summary>
        /// standard(内置分词)
        /// </summary>
        [Description("standard")]
        standard = 1,
        /// <summary>
        /// simple(内置分词)
        /// </summary>
        [Description("simple")]
        simple = 2,
        /// <summary>
        /// whitespace(内置分词)
        /// </summary>
        [Description("whitespace")]
        whitespace = 3,
        /// <summary>
        /// stop(内置分词)
        /// </summary>
        [Description("stop")]
        stop = 4,
        /// <summary>
        /// language(内置分词)
        /// </summary>
        [Description("language")]
        language = 5,
        /// <summary>
        /// pattern(内置分词)
        /// </summary>
        [Description("pattern")]
        pattern = 6,
        /// <summary>
        /// ik_max_word(中文分词)
        /// </summary>
        [Description("ik_max_word")]
        ik_max_word = 7,
        /// <summary>
        /// ik_smart(中文分词)
        /// </summary>
        [Description("ik_smart")]
        ik_smart = 8,
    }
}
