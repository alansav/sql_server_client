using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public interface IDbSession
    {
        void Commit();
        void Rollback();
        void AddCommandToSession(SqlCommand command);
    }
}
