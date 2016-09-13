using System;
using System.Threading.Tasks;
using Moq;
using Savage.SqlServerClient.ResultSets;
using Xunit;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningScalarTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Broker_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlQueryReturningScalar<FakeStoredProcedure>(null));
        }

        /*
        [Fact]
        public async Task Execute_Should_Call_Broker_ExecuteScalar()
        {
            //Arrange
            var fakeResult = new object();
            var mockBroker = new Mock<IBroker<FakeStoredProcedure>>();
            mockBroker.Setup(x => x.ExecuteScalarAsync()).ReturnsAsync(fakeResult);
            var sut = new SqlQueryReturningScalar<FakeStoredProcedure>(mockBroker.Object);

            //Act
            var actualResult = await sut.ExecuteAsync();

            //Assert
            mockBroker.Verify(x => x.ExecuteScalarAsync(), Times.Once);
            Assert.Same(fakeResult, actualResult);
        }*/
    }
}
