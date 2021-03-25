using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore
{
    /// <summary>
    /// 基类
    /// </summary>
    [Serializable]
    [Index(nameof(Id))]//索引
    public abstract class EntityModel
    {
        /// <summary>
        /// 主键ID（自增）
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     //   [Column("主键Id")]
        [Description("Id")]
        public virtual int Id { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Description("是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(100)]
        [Required]
        [Description("创建人")]
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [MaxLength(100)]
        [Description("最后更新人")]
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Description("最后更时间")]
        public DateTime? UpdateTime { get; set; }
    }
}
