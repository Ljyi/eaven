using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        string ProviderName { get; }
        /// <summary>
        ///获取 当前单元操作是否已被提交
        /// </summary>
        bool IsCommitted { get; }

        #region 方法
        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// 事务开启
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 事务开启
        /// </summary>
        /// <param name="isolationLevel"></param>
        void BeginTransaction(IsolationLevel isolationLevel);
        /// <summary>
        /// 事务提交
        /// </summary>
        void TransactionCommit();
        /// <summary>
        /// 事务回滚
        /// </summary>
        void TransactionRollback();
        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>

        #endregion
        //IDbContextTransaction BeginTransaction();

        //IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);

        //Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        //Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    }
}
