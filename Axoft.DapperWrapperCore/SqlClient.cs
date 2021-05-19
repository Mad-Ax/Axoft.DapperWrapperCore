namespace Axoft.DapperWrapperCore
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using DapperExtensions;
    using DapperExtensions.Sql;

    public class SqlClient : BaseClient
    {
        public SqlClient(string connectionString) : base(connectionString)
        {
        }

        protected override IDbConnection GetDbConnection()
        {
            DapperExtensions.SqlDialect = new SqlServerDialect();

            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection(this.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create SqlConnection.  Check the connection string is correct", ex);
            }
            sqlConnection.Open();
            return sqlConnection;
        }

        protected override async Task<IDbConnection> GetDbConnectionAsync()
        {
            DapperExtensions.SqlDialect = new SqlServerDialect();

            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection(this.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot create SqlConnection.  Check the connection string is correct", ex);
            }
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}
