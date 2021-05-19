namespace Axoft.DapperWrapperCore.SqliteTests.Integration.SqliteClientTests
{
    using System;

    public abstract class TestsBase : IDisposable
    {
        protected readonly DatabaseFixture _fixture;
        protected readonly TestDataHelper _testDataHelper;

        public TestsBase(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _testDataHelper = new TestDataHelper(_fixture.Connection);
        }

        public void Dispose()
        {
            _testDataHelper.CleanUp();
        }
    }
}
