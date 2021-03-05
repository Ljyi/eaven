using System;
using System.Threading.Tasks;

namespace Eaven.Ven.Minio
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public interface IMinioService
    {
        Task<bool> FileUpload(string filePath, string fileName, string bucketName);
    }
}
