using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public class FakeStoredProcedure : StoredProcedure
    {
        public FakeStoredProcedure()
            : base("storedProcedure")
        {
        }
    }

    public class FakeParameters : IParameters<FakeStoredProcedure>
    {
        public readonly int Id;

        public FakeParameters(int id)
        {
            Id = id;
        }

        public SqlParameter[] ToSqlParameters()
        {
            return new [] { new SqlParameter("@Id", Id) };
        }
    }
}
