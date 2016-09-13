using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Savage.SqlServerClient.ResultSets;

namespace Savage.SqlServerClient
{
    public class Broker<T> : IBroker<T> where T : ISqlQuery
    {
        public async Task<object> ExecuteScalarAsync(SqlTransaction transaction, T query, IParameters<T> parameters)
        {
            var command = await BuildCommand(transaction, query, parameters);
            return await command.ExecuteScalarAsync();
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(SqlTransaction transaction, T query, IParameters<T> parameters)
        {
            var command = await BuildCommand(transaction, query, parameters);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return new RowsAffectedResultSet(rowsAffected);
        }

        public async Task<IResultSet<T>> ExecuteDataReaderAsync(SqlTransaction transaction, T query, IParameters<T> parameters, IDataReaderHandler<T> handler)
        {
            var command = await BuildCommand(transaction, query, parameters);

            using (var reader = await command.ExecuteReaderAsync())
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }

        private static async Task<SqlCommand> BuildCommand(SqlTransaction transaction, T query, IParameters<T> parameters)
        {
            var commandBuilder = new CommandBuilder<T>();
            var command = commandBuilder.BuildSqlCommand(transaction, query, parameters);

            await OpenConnectionIfNotAlreadyOpen(command.Connection);
            return command;
        }

        private static async Task OpenConnectionIfNotAlreadyOpen(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }
    }
}
