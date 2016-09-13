using System;
using System.Data;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public class StoredProcedure : ISqlQuery
    {
        public readonly string StoredProcedureName;

        protected StoredProcedure(string storedProcedureName)
        {
            StoredProcedureName = storedProcedureName;
        }

        public SqlCommand CreateSqlCommand()
        {
            return new SqlCommand(StoredProcedureName)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
    }
}
