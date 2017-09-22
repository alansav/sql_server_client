using System;
using System.Data;
using Moq;
using Xunit;

namespace Savage.SqlServerClient
{
    public class OptimizedDataReaderTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_dataReader_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Data.OptimizedDataReader(null));
        }

        [Fact]
        public void Read_Should_Call_Read_On_DataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read());

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            sut.Read();

            mockDataReader.Verify(x=>x.Read(), Times.Once);
        }

        [Fact]
        public void NextResult_Should_Call_NextResult_On_DataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.NextResult());

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            sut.NextResult();

            mockDataReader.Verify(x => x.NextResult(), Times.Once);
        }

        [Fact]
        public void GetBoolean_Should_Call_GetBoolean()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("boolean")).Returns(8);
            mockDataReader.Setup(x => x.GetBoolean(8)).Returns(true);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetBoolean("boolean");

            Assert.Equal(true, result);
            mockDataReader.Verify(x => x.GetOrdinal("boolean"), Times.Once);
            mockDataReader.Verify(x => x.GetBoolean(8), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetBoolean("boolean");

            Assert.Equal(true, result2);
            mockDataReader.Verify(x=>x.GetOrdinal("boolean"), Times.Never);
            mockDataReader.Verify(x => x.GetBoolean(8), Times.Once);
        }

        [Fact]
        public void GetNullableBoolean_Should_Return_Null_When_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("boolean")).Returns(8);
            mockDataReader.Setup(x => x.IsDBNull(8)).Returns(true);
            mockDataReader.Setup(x => x.GetBoolean(8));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableBoolean("boolean");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("boolean"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(8), Times.Once);
            mockDataReader.Verify(x => x.GetBoolean(8), Times.Never);
        }

        [Fact]
        public void GetNullableBoolean_Should_Return_True_When_IsDbNull_Returns_False()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("boolean")).Returns(8);
            mockDataReader.Setup(x => x.IsDBNull(8)).Returns(false);
            mockDataReader.Setup(x => x.GetBoolean(8)).Returns(true);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableBoolean("boolean");

            Assert.Equal(true, result);
            mockDataReader.Verify(x => x.GetOrdinal("boolean"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(8), Times.Once);
            mockDataReader.Verify(x => x.GetBoolean(8), Times.Once);
        }

        [Fact]
        public void GetString_Should_Return_Value_Wehn_IsDbNull_Returns_False()
        {
            var fakeResult = "abcdef";
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("string")).Returns(9);
            mockDataReader.Setup(x => x.IsDBNull(9)).Returns(false);
            mockDataReader.Setup(x => x.GetString(9)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetString("string");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("string"), Times.Once);
            mockDataReader.Verify(x => x.GetString(9), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetString("string");

            Assert.Equal(fakeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("string"), Times.Never);
            mockDataReader.Verify(x => x.GetString(9), Times.Once);
        }

        [Fact]
        public void GetString_Should_Return_Null_Wehn_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("string")).Returns(9);
            mockDataReader.Setup(x => x.IsDBNull(9)).Returns(true);
            mockDataReader.Setup(x => x.GetString(9));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetString("string");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("string"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(9), Times.Once);
            mockDataReader.Verify(x => x.GetString(9), Times.Never);
        }

        [Fact]
        public void GetDateTime_Should_Call_GetDateTime()
        {
            var fakeDateTimeResult = new DateTime(2016,9,12,22,0,0);
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("datetime")).Returns(10);
            mockDataReader.Setup(x => x.GetDateTime(10)).Returns(fakeDateTimeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetDateTime("datetime");

            Assert.Equal(fakeDateTimeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("datetime"), Times.Once);
            mockDataReader.Verify(x => x.GetDateTime(10), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetDateTime("datetime");

            Assert.Equal(fakeDateTimeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("datetime"), Times.Never);
            mockDataReader.Verify(x => x.GetDateTime(10), Times.Once);
        }

        [Fact]
        public void GetNullableDateTime_Should_Return_Value_When_IsDbNull_Returns_False()
        {
            var fakeDateTimeResult = new DateTime(2016, 9, 12, 22, 0, 0);
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("datetime")).Returns(11);
            mockDataReader.Setup(x => x.IsDBNull(11)).Returns(false);
            mockDataReader.Setup(x => x.GetDateTime(11)).Returns(fakeDateTimeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableDateTime("datetime");

            Assert.Equal(fakeDateTimeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("datetime"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(11), Times.Once);
            mockDataReader.Verify(x => x.GetDateTime(11), Times.Once);
        }

        [Fact]
        public void GetNullableDateTime_Should_Return_Null_When_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("datetime")).Returns(11);
            mockDataReader.Setup(x => x.IsDBNull(11)).Returns(true);
            mockDataReader.Setup(x => x.GetDateTime(11));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableDateTime("datetime");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("datetime"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(11), Times.Once);
            mockDataReader.Verify(x => x.GetDateTime(11), Times.Never);
        }

        [Fact]
        public void GetInt32_Should_Call_GetInt32()
        {
            const int fakeResult = int.MaxValue;
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int32")).Returns(12);
            mockDataReader.Setup(x => x.GetInt32(12)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetInt32("int32");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("int32"), Times.Once);
            mockDataReader.Verify(x => x.GetInt32(12), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetInt32("int32");

            Assert.Equal(fakeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("int32"), Times.Never);
            mockDataReader.Verify(x => x.GetInt32(12), Times.Once);
        }

        [Fact]
        public void GetNullableInt32_Should_Return_Value_When_IsDbNull_Returns_False()
        {
            const int fakeResult = int.MaxValue;
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int32")).Returns(12);
            mockDataReader.Setup(x => x.IsDBNull(12)).Returns(false);
            mockDataReader.Setup(x => x.GetInt32(12)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableInt32("int32");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("int32"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(12), Times.Once);
            mockDataReader.Verify(x => x.GetInt32(12), Times.Once);
        }

        [Fact]
        public void GetNullableInt32_Should_Return_Null_When_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int32")).Returns(12);
            mockDataReader.Setup(x => x.IsDBNull(12)).Returns(true);
            mockDataReader.Setup(x => x.GetInt32(12));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableInt32("int32");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("int32"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(12), Times.Once);
            mockDataReader.Verify(x => x.GetInt32(12), Times.Never);
        }

        [Fact]
        public void GetInt64_Should_Call_GetInt64()
        {
            const long fakeResult = long.MaxValue;
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int64")).Returns(13);
            mockDataReader.Setup(x => x.GetInt64(13)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetInt64("int64");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("int64"), Times.Once);
            mockDataReader.Verify(x => x.GetInt64(13), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetInt64("int64");

            Assert.Equal(fakeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("int64"), Times.Never);
            mockDataReader.Verify(x => x.GetInt64(13), Times.Once);
        }

        [Fact]
        public void GetNullableInt64_Should_Return_Value_When_IsDbNull_Returns_False()
        {
            const long fakeResult = long.MaxValue;
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int64")).Returns(13);
            mockDataReader.Setup(x => x.IsDBNull(13)).Returns(false);
            mockDataReader.Setup(x => x.GetInt64(13)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableInt64("int64");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("int64"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(13), Times.Once);
            mockDataReader.Verify(x => x.GetInt64(13), Times.Once);
        }

        [Fact]
        public void GetNullableInt64_Should_Return_Null_When_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("int64")).Returns(13);
            mockDataReader.Setup(x => x.IsDBNull(13)).Returns(true);
            mockDataReader.Setup(x => x.GetInt64(13));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableInt64("int64");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("int64"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(13), Times.Once);
            mockDataReader.Verify(x => x.GetInt32(13), Times.Never);
        }

        [Fact]
        public void GetGuid_Should_Call_GetGuid()
        {
            var fakeResult = new Guid("E7DA6396-1C6F-4130-A725-C50EBD2C7CB9");
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("guid")).Returns(14);
            mockDataReader.Setup(x => x.GetGuid(14)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetGuid("guid");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("guid"), Times.Once);
            mockDataReader.Verify(x => x.GetGuid(14), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetGuid("guid");

            Assert.Equal(fakeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("guid"), Times.Never);
            mockDataReader.Verify(x => x.GetGuid(14), Times.Once);
        }

        [Fact]
        public void GetNullableGuid_Should_Return_Value_When_IsDbNull_Returns_False()
        {
            var fakeResult = new Guid("E7DA6396-1C6F-4130-A725-C50EBD2C7CB9");
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("guid")).Returns(14);
            mockDataReader.Setup(x => x.IsDBNull(14)).Returns(false);
            mockDataReader.Setup(x => x.GetGuid(14)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableGuid("guid");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("guid"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(14), Times.Once);
            mockDataReader.Verify(x => x.GetGuid(14), Times.Once);
        }

        [Fact]
        public void GetNullableGuid_Should_Return_Null_When_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("guid")).Returns(14);
            mockDataReader.Setup(x => x.IsDBNull(14)).Returns(true);
            mockDataReader.Setup(x => x.GetGuid(14));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetNullableGuid("guid");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("guid"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(14), Times.Once);
            mockDataReader.Verify(x => x.GetGuid(14), Times.Never);
        }

        [Fact]
        public void GetBytes_Should_Return_Value_Wehn_IsDbNull_Returns_False()
        {
            byte[] fakeResult = new byte[1];
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("bytes")).Returns(15);
            mockDataReader.Setup(x => x.IsDBNull(15)).Returns(false);
            mockDataReader.Setup(x => x.GetValue(15)).Returns(fakeResult);

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetBytes("bytes");

            Assert.Equal(fakeResult, result);
            mockDataReader.Verify(x => x.GetOrdinal("bytes"), Times.Once);
            mockDataReader.Verify(x => x.GetValue(15), Times.Once);

            //Query the same row shouldn't call GetOrdinal
            mockDataReader.ResetCalls();
            var result2 = sut.GetBytes("bytes");

            Assert.Equal(fakeResult, result2);
            mockDataReader.Verify(x => x.GetOrdinal("bytes"), Times.Never);
            mockDataReader.Verify(x => x.GetValue(15), Times.Once);
        }

        [Fact]
        public void GetBytes_Should_Return_Null_Wehn_IsDbNull_Returns_True()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.Setup(x => x.GetOrdinal("bytes")).Returns(15);
            mockDataReader.Setup(x => x.IsDBNull(15)).Returns(true);
            mockDataReader.Setup(x => x.GetValue(15));

            var sut = new Data.OptimizedDataReader(mockDataReader.Object);
            var result = sut.GetBytes("bytes");

            Assert.Equal(null, result);
            mockDataReader.Verify(x => x.GetOrdinal("bytes"), Times.Once);
            mockDataReader.Verify(x => x.IsDBNull(15), Times.Once);
            mockDataReader.Verify(x => x.GetString(15), Times.Never);
        }
    }
}
