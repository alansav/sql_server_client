using System;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public abstract class RepositoryBaseClass
    {
        protected readonly SqlTransaction Transaction;

        protected RepositoryBaseClass(SqlTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            Transaction = transaction;
        }
    }
}