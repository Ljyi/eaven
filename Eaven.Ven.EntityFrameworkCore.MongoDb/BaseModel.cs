using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb
{
    public class BaseModel
    {
        [BsonId]        //标记主键
        [BsonRepresentation(BsonType.ObjectId)]     //参数类型  ， 无需赋值
        public string Id { get; set; }
        //指明数据库中字段名为CreateDateTime
        [BsonElement(nameof(CreateDateTime))]
        public DateTime CreateDateTime { get; set; }

        //[BsonElement(nameof(IsDelete))]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        public BaseModel()
        {
            CreateDateTime = DateTime.Now;
            IsDelete = false;
        }
    }
}
