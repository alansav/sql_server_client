using Xunit;

namespace Savage.Data.SqlServerClient
{
    public class DbClientTests
    {
        [Fact]
        public void Constructor_instantiates_SqlCommandExecutor()
        {
            var sut = new DbClient("Server=.;Database=db;Trusted_Connection=True;");
            Assert.NotNull(sut.CommandExecutor);
        }
        
        [Fact]
        public void CreateDbSession_returns_DbSession()
        {
            var sut = new DbClient("Server=.;Database=db;Trusted_Connection=True;");

            var dbSession = sut.CreateDbSession();
            Assert.NotNull(dbSession);
        }
    }
}
