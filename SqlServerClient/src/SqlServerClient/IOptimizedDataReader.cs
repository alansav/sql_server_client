using System;

namespace Savage.SqlServerClient
{
    public interface IOptimizedDataReader
    {
        bool Read();
        bool NextResult();
        bool GetBoolean(string columnName);
        bool? GetNullableBoolean(string columnName);
        string GetString(string columnName);
        DateTime GetDateTime(string columnName);
        DateTime? GetNullableDateTime(string columnName);
        int GetInt32(string columnName);
        int? GetNullableInt32(string columnName);
        long GetInt64(string columnName);
        long? GetNullableInt64(string columnName);
        Guid GetGuid(string columnName);
        Guid? GetNullableGuid(string columnName);
        byte[] GetBytes(string columnName);
    }
}
