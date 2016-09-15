using System;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Savage.SqlServerClient
{
    public class CommandBuilderTests
    {
        [Fact]
        public void BuildSqlCommand_Should_Throw_ArgumentNullException_When_StoredProcedureName_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => CommandBuilder.BuildSqlCommand(null));
        }

        [Fact]
        public void BuildSqlCommand_Should_Throw_ArgumentNullException_When_StoredProcedureName_Is_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CommandBuilder.BuildSqlCommand(string.Empty));
        }

        [Fact]
        public void BuildSqlCommand_Should_Assign_StoredProcedureName_To_CommandText()
        {
            var sqlCommand = CommandBuilder.BuildSqlCommand("stored_procedure");

            Assert.Equal("stored_procedure", sqlCommand.CommandText);
        }

        [Fact]
        public void BuildSqlCommand_Should_Set_CommandType_To_StoredProcedure()
        {
            var sqlCommand = CommandBuilder.BuildSqlCommand("stored_procedure");

            Assert.Equal(CommandType.StoredProcedure, sqlCommand.CommandType);
        }
        
        [Fact]
        public void BuildSqlCommand_Should_Set_Parameters()
        {
            var fakeParameters = new[]
            {
                new SqlParameter("Parameter1", 123),
                new SqlParameter("Parameter2", 222),
            };
            var sut = CommandBuilder.BuildSqlCommand("stored_procedure", fakeParameters);

            Assert.Same(fakeParameters[0], sut.Parameters[0]);
            Assert.Same(fakeParameters[1], sut.Parameters[1]);
        }
    }
}
