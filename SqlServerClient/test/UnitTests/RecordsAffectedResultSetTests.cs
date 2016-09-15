using Xunit;

namespace Savage.SqlServerClient
{
    public class RecordsAffectedResultSetTests
    {
        [Fact]
        public void ConstructorShouldSetRowsAffectedProperty()
        {
            var sut = new RowsAffectedResultSet(1234);
            Assert.Equal(1234, sut.RowsAffected);
        }
    }
}
