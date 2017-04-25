using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Savage.SqlServerClient
{
    public class OptimizedDataReader : IOptimizedDataReader
    {
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        private readonly IDataReader _dataReader;

        public OptimizedDataReader(IDataReader dataReader)
        {
            _dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }

        public bool Read()
        {
            return _dataReader.Read();
        }

        public bool NextResult()
        {
            Reset();
            return _dataReader.NextResult();
        }

        private void Reset()
        {
            _dictionary = new Dictionary<string, int>();
        }

        private int GetOrdinal(string columnName)
        {
            if (_dictionary.Keys.Contains(columnName))
            {
                return _dictionary[columnName];
            }

            var index = _dataReader.GetOrdinal(columnName);
            _dictionary.Add(columnName, index);
            return index;
        }

        public bool GetBoolean(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.GetBoolean(ordinal);
        }

        public bool? GetNullableBoolean(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }

            return _dataReader.GetBoolean(ordinal);
        }

        public string GetString(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.IsDBNull(ordinal) ? null : _dataReader.GetString(ordinal);
        }

        public DateTime GetDateTime(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.GetDateTime(ordinal);
        }

        public DateTime? GetNullableDateTime(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }
            return _dataReader.GetDateTime(ordinal);
        }

        public int GetInt32(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.GetInt32(ordinal);
        }

        public int? GetNullableInt32(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }
            return _dataReader.GetInt32(ordinal);
        }

        public long GetInt64(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.GetInt64(ordinal);
        }

        public long? GetNullableInt64(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }
            return _dataReader.GetInt64(ordinal);
        }

        public Guid GetGuid(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            return _dataReader.GetGuid(ordinal);
        }

        public Guid? GetNullableGuid(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }
            return _dataReader.GetGuid(ordinal);
        }
        
        public byte[] GetBytes(string columnName)
        {
            var ordinal = GetOrdinal(columnName);
            if (_dataReader.IsDBNull(ordinal))
            {
                return null;
            }
            //TODO: Should be calling the GetBytes method but this is not as straight forward as the other types. Optimize if/when needed and not before
            return (byte[])_dataReader.GetValue(ordinal);
        }
    }
}
