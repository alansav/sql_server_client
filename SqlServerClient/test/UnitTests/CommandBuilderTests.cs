using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Savage.SqlServerClient
{
    public class CommandBuilderTests
    {
        [Fact]
        public void BuildSqlCommand_Should_Throw_ArgumentNullException_When_Transaction_Is_Null()
        {
            var fakeParameters = new FakeParameters(1);
            var sut = new CommandBuilder<FakeStoredProcedure>();

            Assert.Throws<ArgumentNullException>(
                () => sut.BuildSqlCommand(null, new FakeStoredProcedure(), fakeParameters));
        }
    }
}
