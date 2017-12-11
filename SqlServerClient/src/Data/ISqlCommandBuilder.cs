using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Savage.Data
{
    public interface ISqlCommandBuilder
    {
        IDbCommand BuildSqlCommandForStoredProcedure(string storedProcedureName);
        IDbCommand BuildSqlCommandForSql(string sql);
    }
}
