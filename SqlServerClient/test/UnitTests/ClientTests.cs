using System;
using System.Data;
using Xunit;

namespace Savage.SqlServerClient
{
    public class ClientTests
    {
        [Fact]
        public void ArgumentNullException_Thrown_When_BuildStoredProcedureCommand_ProcedureName_Is_Null()
        {
            //Act
            Assert.Throws<ArgumentNullException>(() => Client.BuildStoredProcedureCommand(null));
        }

        [Fact]
        public void ArgumentException_Thrown_When_BuildStoredProcedureCommand_ProcedureName_Is_EmptyString()
        {
            //Act
            Assert.Throws<ArgumentException>(() => Client.BuildStoredProcedureCommand("    "));
        }

        [Fact]
        public void BuildStoredProcedureCommand_Creates_SqlCommand_Correctly()
        {
            //Arrange
            var storedProcedureName = "testStoredProcedure";

            //Act
            var sut = Client.BuildStoredProcedureCommand(storedProcedureName);

            //Assert
            Assert.Equal(storedProcedureName, sut.CommandText);
            Assert.Equal(CommandType.StoredProcedure, sut.CommandType);
        }

        /* TODO: Reinstate this test once a mock framework has been chosen
        [TestMethod]
        public void ExecuteNonQuery_CallsExecuteNonQuery()
        {
            //Arrange
            var mockConnection = MockRepository.GenerateMock<IDbConnection>();
            mockConnection.Stub(x=>x.State).Return(ConnectionState.Closed);
            mockConnection.Expect(x => x.Open()).Repeat.Once();

            var mockCommand = MockRepository.GenerateMock<IDbCommand>();
            mockCommand.Expect(x => x.ExecuteNonQuery()).Return(123).Repeat.Once();
            mockCommand.Stub(x => x.Connection);

            //Act
            var sut = Common.SqlServer.SqlServerWrapper.ExecuteNonQuery(mockConnection, mockCommand);

            //Assert
        //    Assert.AreEqual(mockConnection, mockCommand.Connection);
            Assert.AreEqual(123, sut);
        }*/
    }
}
