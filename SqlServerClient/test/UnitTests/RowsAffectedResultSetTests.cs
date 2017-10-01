using Xunit;

namespace Savage.Data
{
    public class RowsAffectedResultSetTests
    {
        [Fact]
        public void ConstructorShouldSetRowsAffectedProperty()
        {
            var sut = new RowsAffectedResultSet(1234);
            Assert.Equal(1234, sut.RowsAffected);
        }
    }
}
