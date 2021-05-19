namespace Axoft.DapperWrapperCore.SqliteTests.Integration
{
    using System;
    using Microsoft.Data.Sqlite;

    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Connection = new SqliteConnection("Data Source=integration_test.db");
            Connection.Open();

            string sql = "drop table if exists testmodel";
            var command = new SqliteCommand(sql, Connection);
            command.ExecuteNonQuery();

            sql = "create table testmodel (id integer primary key, name varchar(20), type varchar(20))";
            command = new SqliteCommand(sql, Connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            var sql = "drop table if exists testmodel";
            var command = new SqliteCommand(sql, Connection);
            command.ExecuteNonQuery();
        }

        public SqliteConnection Connection { get; private set; }
    }
}
