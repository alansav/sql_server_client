using System;
using System.Data.SqlClient;
using Moq;
using Xunit;

namespace Savage.SqlServerClient
{
    public class CommandExecutorTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_dbSession_Is_Null()
        {
            var sqlCommand = new SqlCommand();
            Assert.Throws<ArgumentNullException>(() => new CommandExecutor(null, sqlCommand));
        }

        [Fact]
        public void Constructor_SHould_Throw_ArgumentNullException_When_command_Is_Null()
        {
            var mockDbSession = new Mock<IDbSession>();
            Assert.Throws<ArgumentNullException>(() => new CommandExecutor(mockDbSession.Object, null));
        }

        [Fact]
        public void Constructor2_Should_Throw_ArgumentNullException_When_dbSession_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new CommandExecutor(null));
        }
    }
}
