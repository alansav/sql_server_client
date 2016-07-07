using System;
using Xunit;

namespace Savage.SqlServerClient
{
    public class ExecuteStoredProcedureTests
    {
        [Fact]
        public void ArgumentNullException_Thrown_When_Transaction_Is_Null()
        {
            //Arrange

            //Act
            Assert.Throws<ArgumentNullException>(() => ExecuteStoredProcedure<Concrete>.Execute(null, null));
        }

        private class Concrete : StoredProcedure
        {
            public Concrete(string storedProcedureName)
                : base(storedProcedureName)
            {
            }
        }
    }
}