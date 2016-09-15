namespace Savage.SqlServerClient
{
    public interface IDataReaderHandler
    {
        IResultSet Handle(IOptimizedDataReader optimizedDataReader);
    }
}
