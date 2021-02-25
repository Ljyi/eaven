using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.Uow
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(TDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string ProviderName => _dbContext.Database.ProviderName;

        public bool IsCommitted { get; private set; }
        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {
                int result = _dbContext.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)//
            {
                //更新异常做重试机制
                string msg = e.Message;
                if (e.InnerException != null)
                {
                    msg = "异常信息：" + msg + "InnerException:" + e.InnerException.Message.ToString();
                }
                throw e;
            }
        }
        /// <summary>
        ///  事务开启
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                _dbTransaction = _dbContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  事务开启
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _dbTransaction = _dbContext.Database.BeginTransaction(isolationLevel);
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            _dbTransaction?.Rollback();
        }
        /// <summary>
        /// 事务释放
        /// </summary>
        public void Dispose()
        {
            _dbTransaction?.Dispose();
        }
        /// <summary>
        /// 事务提交
        /// </summary>
        public void TransactionCommit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 事务释放
        /// </summary>
        public void TransactionRollback()
        {
            try
            {
                _dbTransaction?.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
