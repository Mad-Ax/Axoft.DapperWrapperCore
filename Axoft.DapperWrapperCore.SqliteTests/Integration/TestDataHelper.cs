namespace Axoft.DapperWrapperCore.SqliteTests.Integration
{
    using System;
    using System.Data;
    using Microsoft.Data.Sqlite;

    public class TestDataHelper
    {
        private readonly SqliteConnection _connection;

        public TestDataHelper(SqliteConnection connection)
        {
            _connection = connection;
        }

        public int InsertTestModel(string name, string type = "default")
        {
            var sql = "insert into testmodel (name, type) values (@name, @type); select last_insert_rowid()";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("name", name));
                command.Parameters.Add(new SqliteParameter("type", type));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void CleanUp()
        {
            var sql = "delete from testmodel";
            var command = new SqliteCommand(sql, _connection);
            command.ExecuteNonQuery();
        }
    }
}
