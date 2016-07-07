using Xunit;

namespace Savage.SqlServerClient
{
    public class StoredProcedureTest
    {
        private class ConcreteStoredProcedure : StoredProcedure
        {
            public ConcreteStoredProcedure(string storedProcedureName)
                : base(storedProcedureName)
            {
            }
        }

        [Fact]
        public void StoredProcedureNameAssignedCorrectly()
        {
            //Arrange
            string storedProcedureName = "who2";

            //Act
            var sut = new ConcreteStoredProcedure(storedProcedureName);

            //Assert
            Assert.Equal(storedProcedureName, sut.StoredProcedureName);
        }
    }
}
