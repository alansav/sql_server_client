using System;
using System.Threading.Tasks;
using Moq;
using Savage.SqlServerClient.ResultSets;
using Xunit;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningResultSetTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Broker_Is_Null()
        {
            var mockDataReaderHandler = new Mock<IDataReaderHandler<FakeStoredProcedure>>();
            Assert.Throws<ArgumentNullException>(() => new SqlQueryReturningResultSet<FakeStoredProcedure>(null, mockDataReaderHandler.Object));
        }

        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Handler_Is_Null()
        {
            var fakeStoredProcedure = new FakeStoredProcedure();
            Assert.Throws<ArgumentNullException>(() => new SqlQueryReturningResultSet<FakeStoredProcedure>(fakeStoredProcedure, null));
        }

        /*
        [Fact]
        public async Task Execute_Should_Call_Broker_ExecuteDataReader()
        {
            //Arrange
            var fakeResultSet = new Mock<IResultSet<FakeStoredProcedure>>().Object;
            var mockBroker = new Mock<IBroker<FakeStoredProcedure>>();
            var mockDataReaderHandler = new Mock<IDataReaderHandler<FakeStoredProcedure>>();

            mockBroker.Setup(x => x.ExecuteDataReaderAsync(mockDataReaderHandler.Object)).ReturnsAsync(fakeResultSet);
            var sut = new SqlQueryReturningResultSet<FakeStoredProcedure>(mockBroker.Object, mockDataReaderHandler.Object);

            //Act
            var actualResultSet = await sut.ExecuteAsync();

            //Assert
            mockBroker.Verify(x => x.ExecuteDataReaderAsync(mockDataReaderHandler.Object), Times.Once);
            Assert.Same(fakeResultSet, actualResultSet);
        }*/
    }
}
