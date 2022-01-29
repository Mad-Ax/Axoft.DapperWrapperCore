namespace Axoft.DapperWrapperCore.SqliteTests.Integration.SqliteClientTests
{
    using Xunit;

    [Collection(CollectionTypes.DatabaseTests)]
    public class QueryTests : TestsBase
    {
        public QueryTests(DatabaseFixture fixture) : base(fixture)
        { }

        // TODO: other tests not written - just these ones to prove a bug in DapperExtensions

        [Fact]
        public void QueryOne_WithNoParameter_ReturnsOneRecord()
        {
            // Arrange
            var id = _testDataHelper.InsertTestModel("test data for queryone");

            var client = new TestClient();

            // Act
            var result = client.QueryOne();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void QueryOne_WithParameter_ReturnsOneRecord()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("test data 1 for queryone");
            var id2 = _testDataHelper.InsertTestModel("test data 2 for queryone");
            var id3 = _testDataHelper.InsertTestModel("test data 3 for queryone");

            var client = new TestClient();

            // Act
            var result = client.QueryOne(id2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id2, result.Id);
        }
    }
}
