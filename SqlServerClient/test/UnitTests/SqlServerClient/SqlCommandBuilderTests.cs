using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Savage.Data.SqlServerClient
{
    public class SqlCommandBuilderTests
    {
        [Fact]
        public void BuildCommand_Should_Throw_ArgumentNullException_When_StoredProcedureName_Is_Null()
        {
            var sut = new SqlCommandBuilder();
            Assert.Throws<ArgumentNullException>(() => sut.BuildCommand(null));
        }
        
        [Fact]
        public void BuildCommand_Should_Set_CommandText_to_StoredProcedure_Name()
        {
            var expectedStoredProcedureName = "store_proc_1";
            var mockStoredProcedure = new Mock<IStoredProcedure>();
            mockStoredProcedure.SetupGet(x => x.StoredProcedureName).Returns(expectedStoredProcedureName);

            var sut = new SqlCommandBuilder();
            var command = sut.BuildCommand(mockStoredProcedure.Object);

            Assert.Equal(expectedStoredProcedureName, command.CommandText);
        }

        [Fact]
        public void BuildCommand_Should_Set_CommandType_to_StoredProcedure()
        {
            var mockStoredProcedure = new Mock<IStoredProcedure>();
            
            var sut = new SqlCommandBuilder();
            var command = sut.BuildCommand(mockStoredProcedure.Object);

            Assert.Equal(CommandType.StoredProcedure, command.CommandType);
        }


        [Fact]
        public void BuildCommand_Should_Set_Parameters()
        {
            var fakeParameters = new[]
            {
                new SqlParameter("Parameter1", 123),
                new SqlParameter("Parameter2", 222),
            };
            var mockStoredProcedure = new Mock<IStoredProcedure>();
            mockStoredProcedure.SetupGet(x => x.Parameters).Returns(fakeParameters);

            var sut = new SqlCommandBuilder();
            var command = sut.BuildCommand(mockStoredProcedure.Object);

            Assert.Equal(fakeParameters[0], command.Parameters[0]);
            Assert.Equal(fakeParameters[1], command.Parameters[1]);
        }
    }
}
