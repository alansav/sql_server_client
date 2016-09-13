namespace Savage.SqlServerClient.ResultSets
{
    public interface IDataReaderHandler<T> where T : ISqlQuery
    {
        IResultSet<T> Handle(OptimizedDataReader optimizedDataReader);
    }
}
