namespace Axoft.DapperWrapperCore.SqliteTests.Integration
{
    using Xunit;

    [CollectionDefinition(CollectionTypes.DatabaseTests)]
    public class DatabaseCollectionDefinition : ICollectionFixture<DatabaseFixture>
    {
    }
}
