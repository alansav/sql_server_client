using System;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public interface IDbSession : IDisposable
    {
        void Commit();
        void Rollback();
        void AddCommandToSession(SqlCommand command);
    }
}
