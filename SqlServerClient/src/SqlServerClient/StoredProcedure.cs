using System;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public abstract class StoredProcedure
    {
        public readonly string StoredProcedureName;

        protected StoredProcedure(string storedProcedureName)
        {
            StoredProcedureName = storedProcedureName;
        }
    }

    public static class ExecuteStoredProcedure<T> where T : StoredProcedure
    {
        internal static SqlCommand PrepareCommand(SqlTransaction transaction, IParameters<T> parameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var storedProcedure = (StoredProcedure)Activator.CreateInstance(typeof(T));
            var command = Client.BuildStoredProcedureCommand(storedProcedure.StoredProcedureName);
            command.Transaction = transaction;
            if (parameters?.ToSqlParameters() != null)
            {
                command.Parameters.AddRange(parameters.ToSqlParameters());
            }

            return command;
        }

        public static RowsAffectedResultSet Execute(SqlTransaction transaction, IParameters<T> parameters)
        {
            var command = PrepareCommand(transaction, parameters);
            int rows = Client.ExecuteNonQuery(command.Transaction.Connection, command);
            return new RowsAffectedResultSet(rows);
        }

        public static IResultSet<T> ExecuteDataReader(SqlTransaction transaction, IParameters<T> parameters, IDataReaderHandler<T> handler)
        {
            var command = PrepareCommand(transaction, parameters);

            using (var reader = Client.ExecuteReader(command.Transaction.Connection, command))
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }
    }

    public interface IParameters<T> where T : StoredProcedure
    {
        SqlParameter[] ToSqlParameters();
    }

    public interface IResultSet<T> where T : StoredProcedure
    {

    }

    public class RowsAffectedResultSet : IResultSet<StoredProcedure>
    {
        public readonly int RowsAffected;
        public RowsAffectedResultSet(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }
    }

    public interface IDataReaderHandler<T> where T : StoredProcedure
    {
        IResultSet<T> Handle(OptimizedDataReader optimizedDataReader);
    }
}
