using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Moq;
using Savage.SqlServerClient.ResultSets;
using Xunit;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningRowsAffectedTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Broker_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlQueryReturningRowsAffected<FakeStoredProcedure>(null));
        }

        /*
        [Fact]
        public async Task Execute_Should_Call_Broker_ExecuteNonQuery()
        {
            //Arrange
            var fakeRowsAffectedResultSet = new RowsAffectedResultSet(123);
            var mockBroker = new Mock<IBroker<FakeStoredProcedure>>();
            mockBroker.Setup(x => x.ExecuteNonQueryAsync()).ReturnsAsync(fakeRowsAffectedResultSet);
            var sut = new SqlQueryReturningRowsAffected<FakeStoredProcedure>(mockBroker.Object);

            //Act
            var actualRowsAffectedResultSet = await sut.ExecuteAsync();

            //Assert
            mockBroker.Verify(x=>x.ExecuteNonQueryAsync(), Times.Once);
            Assert.Same(fakeRowsAffectedResultSet, actualRowsAffectedResultSet);
        }*/
    }
}
