namespace Axoft.DapperWrapperCore
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using DapperExtensions;
    using DapperExtensions.Sql;
    using Microsoft.Data.Sqlite;

    public class SqliteClient : BaseClient
    {
        public SqliteClient(string connectionString) : base(connectionString)
        {
        }

        protected override IDbConnection GetDbConnection()
        {
            DapperExtensions.SqlDialect = new SqliteDialect();
            DapperAsyncExtensions.SqlDialect = new SqliteDialect();

            SqliteConnection sqliteConnection;
            try
            {
                sqliteConnection = new SqliteConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create SqliteConnection.  Check the connection string is correct", ex);
            }
            sqliteConnection.Open();
            return sqliteConnection;
        }

        protected override async Task<IDbConnection> GetDbConnectionAsync()
        {
            DapperExtensions.SqlDialect = new SqliteDialect();
            DapperAsyncExtensions.SqlDialect = new SqliteDialect();

            SqliteConnection sqliteConnection;
            try
            {
                sqliteConnection = new SqliteConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create SqliteConnection.  Check the connection string is correct", ex);
            }
            await sqliteConnection.OpenAsync();
            return sqliteConnection;
        }
    }
}
