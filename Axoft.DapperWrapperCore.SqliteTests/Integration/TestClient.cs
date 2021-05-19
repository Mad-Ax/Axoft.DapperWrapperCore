namespace Axoft.DapperWrapperCore.SqliteTests.Integration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Axoft.DapperWrapperCore.SqliteTests.Models;
    using DapperExtensions;

    public class TestClient : SqliteClient
    {
        public TestClient() : base("Data Source=integration_test.db")
        { }

        public void Delete(TestModel model)
        {
            base.Delete(model);
        }

        public async Task DeleteAsync(TestModel model)
        {
            await base.DeleteAsync(model);
        }

        public TestModel Get(int id)
        {
            return Get<TestModel>(id);
        }

        public async Task<TestModel> GetAsync(int id)
        {
            return await GetAsync<TestModel>(id);
        }

        public IEnumerable<TestModel> GetAll(object predicate = null, int limit = 0, IList<ISort> sort = null)
        {
            return GetAll<TestModel>(predicate, limit, sort);
        }

        public async Task<IEnumerable<TestModel>> GetAllAsync(object predicate = null, int limit = 0, IList<ISort> sort = null)
        {
            return await GetAllAsync<TestModel>(predicate, limit, sort);
        }
    }
}
