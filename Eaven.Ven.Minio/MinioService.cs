using Microsoft.Extensions.Options;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Minio
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class MinioService : IMinioService
    {
        /// <summary>
        ///请求地址（192.168.9.1）
        /// </summary>
        protected string Url;
        /// <summary>
        /// AccessKey
        /// </summary>
        protected string AccessKey;
        /// <summary>
        /// SecretKey
        /// </summary>
        protected string SecretKey;
        /// <summary>
        /// 
        /// </summary>
        protected bool Secure;

        protected static MinioClient minioClient;
        public MinioService(IOptions<MinioOption> options)
        {
            var connectOption = options.Value;
            if (string.IsNullOrEmpty(connectOption.Endpoint))
            {
                throw new ArgumentException(
                    $"{nameof(connectOption.Endpoint)} cannot be empty or null.");
            }
            if (string.IsNullOrEmpty(connectOption.AccessKey))
            {
                throw new ArgumentException(
                    $"{nameof(connectOption.AccessKey)} cannot be empty or null.");
            }
            if (string.IsNullOrEmpty(connectOption.SecretKey))
            {
                throw new ArgumentException(
                    $"{nameof(connectOption.SecretKey)} cannot be empty or null.");
            }
            Url = connectOption.Endpoint;
            AccessKey = connectOption.AccessKey;
            SecretKey = connectOption.SecretKey;
            Secure = connectOption.Secure;
            if (minioClient == null)
            {
                minioClient = new MinioClient(Url, AccessKey, SecretKey);
                if (Secure)
                {
                    minioClient.WithSSL();
                }
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="bucketName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<bool> FileUpload(string filePath, string fileName, string bucketName)
        {
            try
            {
                var contentType = "application/zip";
                // Make a bucket on the server, if not already present.
                bool found = await minioClient.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minioClient.MakeBucketAsync(bucketName);
                }
                // Upload a file to bucket.
                await minioClient.PutObjectAsync(bucketName, fileName, filePath, contentType);
                Console.WriteLine("Successfully uploaded " + fileName);
                return true;
            }
            catch (MinioException ex)
            {
                throw ex;
                Console.WriteLine("File Upload Error: {0}", ex.Message);
            }
        }
    }
}
