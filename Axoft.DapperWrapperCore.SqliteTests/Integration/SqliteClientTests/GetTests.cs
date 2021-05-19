namespace Axoft.DapperWrapperCore.SqliteTests.Integration.SqliteClientTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Axoft.DapperWrapperCore.SqliteTests.Models;
    using DapperExtensions;
    using Microsoft.Data.Sqlite;
    using Xunit;

    [Collection(CollectionTypes.DatabaseTests)]
    public class GetTests : TestsBase
    {
        public GetTests(DatabaseFixture fixture) : base(fixture)
        { }

        [Fact]
        public void Get_GetsData()
        {
            // Arrange
            var id = _testDataHelper.InsertTestModel("test data for get");

            var client = new TestClient();

            // Act
            var result = client.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("test data for get", result.Name);
        }

        [Fact]
        public async Task GetAsync_GetsData()
        {
            // Arrange
            var id = _testDataHelper.InsertTestModel("test data for get");

            var client = new TestClient();

            // Act
            var result = await client.GetAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("test data for get", result.Name);
        }

        [Fact]
        public void GetAll_GetsAllData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1");
            var id2 = _testDataHelper.InsertTestModel("model 2");
            var id3 = _testDataHelper.InsertTestModel("model 3");

            var client = new TestClient();

            // Act
            var results = client.GetAll();

            // Assert
            Assert.Equal(3, results.Count());
            Assert.Contains(results, r => r.Name == "model 1" && r.Id == id1);
            Assert.Contains(results, r => r.Name == "model 2" && r.Id == id2);
            Assert.Contains(results, r => r.Name == "model 3" && r.Id == id3);
        }

        [Fact]
        public void GetAll_WithPredicate_GetsSelectedData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "default");
            var id2 = _testDataHelper.InsertTestModel("model 2", "special");
            var id3 = _testDataHelper.InsertTestModel("model 3", "special");
            var id4 = _testDataHelper.InsertTestModel("model 4", "default");

            var pred = Predicates.Field<TestModel>(m => m.Type, Operator.Eq, "special");

            var client = new TestClient();

            // Act
            var results = client.GetAll(pred);

            // Assert
            Assert.Equal(2, results.Count());
            Assert.Contains(results, r => r.Name == "model 2" && r.Id == id2);
            Assert.Contains(results, r => r.Name == "model 3" && r.Id == id3);
        }

        [Fact]
        public void GetAll_WithLimit_LimitsSelectedData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "default");
            var id2 = _testDataHelper.InsertTestModel("model 2", "special");
            var id3 = _testDataHelper.InsertTestModel("model 3", "special");
            var id4 = _testDataHelper.InsertTestModel("model 4", "default");

            var client = new TestClient();

            // Act
            var results = client.GetAll(limit: 2);

            // Assert
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void GetAll_WithSort_SortsData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "3");
            var id2 = _testDataHelper.InsertTestModel("model 2", "1");
            var id3 = _testDataHelper.InsertTestModel("model 3", "4");
            var id4 = _testDataHelper.InsertTestModel("model 4", "2");

            var sort = new List<ISort> { Predicates.Sort<TestModel>(m => m.Type) };

            var client = new TestClient();

            // Act
            var results = client.GetAll(sort: sort);

            // Assert
            Assert.Equal(id2, results.ElementAt(0).Id);
            Assert.Equal(id4, results.ElementAt(1).Id);
            Assert.Equal(id1, results.ElementAt(2).Id);
            Assert.Equal(id3, results.ElementAt(3).Id);
        }

        [Fact]
        public async Task GetAllAsync_GetsAllData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1");
            var id2 = _testDataHelper.InsertTestModel("model 2");
            var id3 = _testDataHelper.InsertTestModel("model 3");

            var client = new TestClient();

            // Act
            var results = await client.GetAllAsync();

            // Assert
            Assert.Equal(3, results.Count());
            Assert.Contains(results, r => r.Name == "model 1" && r.Id == id1);
            Assert.Contains(results, r => r.Name == "model 2" && r.Id == id2);
            Assert.Contains(results, r => r.Name == "model 3" && r.Id == id3);
        }

        [Fact]
        public async Task GetAllAsync_WithPredicate_GetsSelectedData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "default");
            var id2 = _testDataHelper.InsertTestModel("model 2", "special");
            var id3 = _testDataHelper.InsertTestModel("model 3", "special");
            var id4 = _testDataHelper.InsertTestModel("model 4", "default");

            var pred = Predicates.Field<TestModel>(m => m.Type, Operator.Eq, "special");

            var client = new TestClient();

            // Act
            var results = await client.GetAllAsync(pred);

            // Assert
            Assert.Equal(2, results.Count());
            Assert.Contains(results, r => r.Name == "model 2" && r.Id == id2);
            Assert.Contains(results, r => r.Name == "model 3" && r.Id == id3);
        }

        [Fact]
        public async Task GetAllAsync_WithLimit_LimitsSelectedData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "default");
            var id2 = _testDataHelper.InsertTestModel("model 2", "special");
            var id3 = _testDataHelper.InsertTestModel("model 3", "special");
            var id4 = _testDataHelper.InsertTestModel("model 4", "default");

            var client = new TestClient();

            // Act
            var results = await client.GetAllAsync(limit: 2);

            // Assert
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetAllAsync_WithSort_SortsData()
        {
            // Arrange
            var id1 = _testDataHelper.InsertTestModel("model 1", "3");
            var id2 = _testDataHelper.InsertTestModel("model 2", "1");
            var id3 = _testDataHelper.InsertTestModel("model 3", "4");
            var id4 = _testDataHelper.InsertTestModel("model 4", "2");

            var sort = new List<ISort> { Predicates.Sort<TestModel>(m => m.Type) };

            var client = new TestClient();

            // Act
            var results = await client.GetAllAsync(sort: sort);

            // Assert
            Assert.Equal(id2, results.ElementAt(0).Id);
            Assert.Equal(id4, results.ElementAt(1).Id);
            Assert.Equal(id1, results.ElementAt(2).Id);
            Assert.Equal(id3, results.ElementAt(3).Id);
        }
    }
}
