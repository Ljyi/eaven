using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Domain
{
    /// <summary>
    /// Blog
    /// </summary>
    //注释
    [Comment("Blogs managed on the website")]
    [Table("Blog")]
    //从模型中排除
    [NotMapped]
    public class Blog
    {
        //实体属性映射到与属性同名
        [Column("blog_id")]
        public int BlogId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Rating { get; set; }
    }
}
