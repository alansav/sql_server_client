using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Savage.Data.SqlServerClient
{
    public class DbClientTests
    {
        private readonly AutoMocker _autoMocker;
        public DbClientTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public void Constructor_instantiates_SqlCommandExecutor()
        {
            var sut = _autoMocker.CreateInstance<DbClient>();
            Assert.NotNull(sut.CommandExecutor);
        }

        [Fact]
        public void CreateDbSession_throws_ArgumentNullException_when_connection_string_is_null()
        {
            var sut = _autoMocker.CreateInstance<DbClient>();

            Action func = () => sut.CreateDbSession(null);
            Assert.Throws<ArgumentNullException>(func);
        }

        [Fact]
        public void CreateDbSession_returns_DbSession()
        {
            var sut = _autoMocker.CreateInstance<DbClient>();

            var dbSession = sut.CreateDbSession("Server=.;Database=db;Trusted_Connection=True;");
            Assert.NotNull(dbSession);
        }
    }
}
