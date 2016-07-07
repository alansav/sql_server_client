using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Savage.SqlServerClient
{
    public class OptimizedDataReader
    {
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        private readonly DbDataReader _dataReader;

        public OptimizedDataReader(DbDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));
            _dataReader = dataReader;
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
            else
            {
                int index = _dataReader.GetOrdinal(columnName);
                _dictionary.Add(columnName, index);
                return index;
            }
        }

        public bool GetBoolean(string columnName)
        {
            return _dataReader.GetBoolean(GetOrdinal(columnName));
        }

        public bool? GetNullableBoolean(string columnName)
        {
            if (_dataReader.IsDBNull(GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return _dataReader.GetBoolean(GetOrdinal(columnName));
            }
        }

        public string GetString(string columnName)
        {
            if (_dataReader.IsDBNull(GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return _dataReader.GetString(GetOrdinal(columnName));
            }
        }

        public DateTime GetDateTime(string columnName)
        {
            return _dataReader.GetDateTime(GetOrdinal(columnName));
        }

        public DateTime? GetNullableDateTime(string columnName)
        {
            if (_dataReader.IsDBNull(GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return _dataReader.GetDateTime(GetOrdinal(columnName));
            }
        }

        public int GetInt32(string columnName)
        {
            return _dataReader.GetInt32(GetOrdinal(columnName));
        }

        public long GetInt64(string columnName)
        {
            return _dataReader.GetInt64(GetOrdinal(columnName));
        }

        public Guid GetGuid(string columnName)
        {
            return _dataReader.GetGuid(GetOrdinal(columnName));
        }

        public Guid? GetNullableGuid(string columnName)
        {
            if (_dataReader.IsDBNull(GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return _dataReader.GetGuid(GetOrdinal(columnName));
            }
        }
        
        public byte[] GetBytes(string columnName)
        {
            //TODO: Should be calling the GetBytes method but this is not as straight forward as the other types. Optimize if/when needed and not before
            return (byte[])_dataReader.GetValue(GetOrdinal(columnName));
        }
    }
}
