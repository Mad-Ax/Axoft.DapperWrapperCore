namespace Axoft.DapperWrapperCore.SqliteTests.Integration.SqliteClientTests
{
    using System;
    using System.Threading.Tasks;
    using Axoft.DapperWrapperCore.SqliteTests.Models;
    using Microsoft.Data.Sqlite;
    using Xunit;

    [Collection(CollectionTypes.DatabaseTests)]
    public class DeleteTests : TestsBase
    {
        public DeleteTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Delete_DeletesData()
        {
            // Arrange
            var id = _testDataHelper.InsertTestModel("test data for deletion");

            var model = new TestModel { Id = id, Name = "test data for deletion" };

            var client = new TestClient();

            // Act
            client.Delete(model);

            // Assert
            var sql = "select count(1) from testmodel";
            var command = new SqliteCommand(sql, _fixture.Connection);
            var result = Convert.ToInt32(command.ExecuteScalar());

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesData()
        {
            // Arrange
            var id = _testDataHelper.InsertTestModel("test data for deletion");

            var model = new TestModel { Id = id, Name = "test data for deletion" };

            var client = new TestClient();

            // Act
            await client.DeleteAsync(model);

            // Assert
            var sql = "select count(1) from testmodel";
            var command = new SqliteCommand(sql, _fixture.Connection);
            var result = Convert.ToInt32(command.ExecuteScalar());

            Assert.Equal(0, result);
        }
    }
}
