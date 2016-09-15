namespace Savage.SqlServerClient
{
    public class RowsAffectedResultSet : IResultSet
    {
        public readonly int RowsAffected;
        public RowsAffectedResultSet(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }
    }
}
