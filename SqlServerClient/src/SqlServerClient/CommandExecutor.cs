using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Savage.SqlServerClient
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDbSession _dbSession;
        private readonly SqlCommand _command;

        public CommandExecutor(IDbSession dbSession, SqlCommand command)
        {
            if (dbSession == null)
                throw new ArgumentNullException(nameof(dbSession));
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _dbSession = dbSession;
            _command = command;

            PrepareSqlCommand(_command);
        }

        private void PrepareSqlCommand(SqlCommand command)
        {
            _dbSession.AddCommandToSession(command);
            SetNullParametersToDbNull(command);
        }

        private SqlCommand BuildAndPrepareSqlCommand(IStoredProcedure storedProcedure)
        {
            var command = CommandBuilder.BuildSqlCommand(storedProcedure);
            PrepareSqlCommand(command);
            return command;
        }

        public CommandExecutor(IDbSession dbSession)
        {
            if (dbSession == null)
                throw new ArgumentNullException(nameof(dbSession));

            _dbSession = dbSession;
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync()
        {
            await OpenConnectionIfNotAlreadyOpen(_command.Connection);
            var rowsAffected = await _command.ExecuteNonQueryAsync();

            return new RowsAffectedResultSet(rowsAffected);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IStoredProcedure storedProcedure)
        {
            var command = BuildAndPrepareSqlCommand(storedProcedure);
            await OpenConnectionIfNotAlreadyOpen(command.Connection);

            var rowsAffected = await _command.ExecuteNonQueryAsync();
            return new RowsAffectedResultSet(rowsAffected);
        }

        public async Task<object> ExecuteScalarAsync()
        {
            await OpenConnectionIfNotAlreadyOpen(_command.Connection);
            var obj = await _command.ExecuteScalarAsync();

            return obj;
        }

        public async Task<object> ExecuteScalarAsync(IStoredProcedure storedProcedure)
        {
            var command = BuildAndPrepareSqlCommand(storedProcedure);
            await OpenConnectionIfNotAlreadyOpen(command.Connection);
            return await command.ExecuteScalarAsync();
        }

        public async Task<IResultSet>  ExecuteReaderAsync(IDataReaderHandler handler)
        {
            await OpenConnectionIfNotAlreadyOpen(_command.Connection);
            using (var reader = await _command.ExecuteReaderAsync())
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }

        public async Task<IResultSet> ExecuteReaderAsync(IStoredProcedure storedProcedure, IDataReaderHandler handler)
        {
            var command = BuildAndPrepareSqlCommand(storedProcedure);
            await OpenConnectionIfNotAlreadyOpen(command.Connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }

        private static void SetNullParametersToDbNull(SqlCommand command)
        {
            //TODO: Investigate if SqlCommand.Parameters can ever be null
            if (command.Parameters == null)
                return;

            foreach (SqlParameter p in command.Parameters)
            {
                if (p.Value == null)
                    p.Value = DBNull.Value;
            }
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
