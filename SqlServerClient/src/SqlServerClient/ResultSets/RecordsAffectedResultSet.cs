namespace Savage.SqlServerClient.ResultSets
{
    public class RowsAffectedResultSet : IResultSet<ISqlQuery>
    {
        public readonly int RowsAffected;
        public RowsAffectedResultSet(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }
    }
}
