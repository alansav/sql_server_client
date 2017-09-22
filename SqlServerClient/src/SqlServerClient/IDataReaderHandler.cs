namespace Savage.SqlServerClient
{
    public interface IDataReaderHandler
    {
        IResultSet Handle(Data.IOptimizedDataReader optimizedDataReader);
    }
}
