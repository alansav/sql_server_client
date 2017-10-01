using System.Collections.Generic;

namespace Savage.Data
{
    public class RowsAffectedResultSet
    {
        public readonly int RowsAffected;
        public RowsAffectedResultSet(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }
    }
}
