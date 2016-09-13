using System.Data;
using Xunit;

namespace Savage.SqlServerClient
{
    public class StoredProcedureTest
    {
        public class Constructor
        {
            [Fact]
            public void StoredProcedureNameAssignedCorrectly()
            {
                const string expectedStoredProcedureName = "storedProcedure";

                //Act
                var sut = new FakeStoredProcedure();

                //Assert
                Assert.Equal(expectedStoredProcedureName, sut.StoredProcedureName);
            }
        }

        public class CreateSqlCommand
        {
            const string ExpectedStoredProcedureName = "storedProcedure";
            private readonly StoredProcedure _storedProcedure;
            public CreateSqlCommand()
            {
                _storedProcedure = new FakeStoredProcedure();
            }
            
            
            [Fact]
            public void Should_Return_SqlCommand_With_StoredProcedureName_As_CommandText()
            {
                //Act
                var sut = _storedProcedure.CreateSqlCommand();

                //Assert
                Assert.Equal(ExpectedStoredProcedureName, sut.CommandText);
            }

            [Fact]
            public void Should_Return_SqlCommand_With_CommandType_StoredProcedure()
            {
                //Act
                var sut = _storedProcedure.CreateSqlCommand();

                //Assert
                Assert.Equal(CommandType.StoredProcedure, sut.CommandType);
            }
        }
    }
}
