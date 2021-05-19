namespace Axoft.DapperWrapperCore
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using DapperExtensions;

    public abstract class BaseClient
    {
        public string ConnectionString { get; set; }

        protected abstract IDbConnection GetDbConnection();

        protected abstract Task<IDbConnection> GetDbConnectionAsync();

        protected BaseClient(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected bool Delete<T>(T model) where T : class
        {
            return WithConnection(c => DapperExtensions.Delete(c, model, null, new int?()));
        }

        protected async Task<bool> DeleteAsync<T>(T model) where T : class
        {
            return await WithConnectionAsync(c => DapperAsyncExtensions.DeleteAsync(c, model, null, new int?()));
        }

        protected T Get<T>(int id) where T : class
        {
            return WithConnection(c => DapperExtensions.Get<T>(c, id, null, new int?()));
        }

        protected async Task<T> GetAsync<T>(int id) where T : class
        {
            return await WithConnectionAsync(c => DapperAsyncExtensions.GetAsync<T>(c, id, null, new int?()));
        }

        protected IEnumerable<T> GetAll<T>(object predicate = null, int limit = 0, IList<ISort> sort = null) where T : class
        {
            return WithConnection(c =>
            {
                var list = (DapperExtensions.GetList<T>(c, predicate, sort, null, new int?(), false));
                if (limit <= 0)
                {
                    return list;
                }

                return list.ToList().GetRange(0, limit);
            });
        }

        protected async Task<IEnumerable<T>> GetAllAsync<T>(object predicate = null, int limit = 0, IList<ISort> sort = null) where T : class
        {
            var list = (await WithConnectionAsync(c => DapperAsyncExtensions.GetListAsync<T>(c, predicate, sort, null, new int?())));
            if (limit <= 0)
            {
                return list;
            }

            return list.ToList().GetRange(0, limit);
        }

        protected T GetOne<T>(object predicate = null, IList<ISort> sort = null) where T : class
        {
            return WithConnection(c => (DapperExtensions.GetList<T>(c, predicate, sort, null, new int?(), false)).FirstOrDefault());
        }

        protected async Task<T> GetOneAsync<T>(object predicate = null, IList<ISort> sort = null) where T : class
        {
            return (await WithConnectionAsync(c => DapperAsyncExtensions.GetListAsync<T>(c, predicate, sort, null, new int?()))).FirstOrDefault();
        }

        protected bool Update<T>(T model) where T : class
        {
            return WithConnection<bool>(c => DapperExtensions.Update(c, model, null, new int?()));
        }

        protected async Task<bool> UpdateAsync<T>(T model) where T : class
        {
            return await WithConnectionAsync(c => DapperAsyncExtensions.UpdateAsync(c, model, null, new int?()));
        }

        protected dynamic Insert<T>(T model) where T : class
        {
            return WithConnection(c => DapperExtensions.Insert(c, model, (IDbTransaction)null, new int?()));
        }

        protected async Task<dynamic> InsertAsync<T>(T model) where T : class
        {
            return await WithConnectionAsync(c => DapperAsyncExtensions.InsertAsync(c, model, null, new int?()));
        }

        protected IEnumerable<T> Query<T>(string sql) where T : class
        {
            return WithConnection(c => c.Query<T>(sql, null, null, true, new int?(), new CommandType?()));
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql) where T : class
        {
            return await WithConnectionAsync(c => c.QueryAsync<T>(sql, null, null, new int?(), new CommandType?()));
        }

        protected T WithConnection<T>(Func<IDbConnection, T> method)
        {
            using (IDbConnection idbConnection = GetDbConnection())
                return method(idbConnection);
        }

        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> method)
        {
            using (IDbConnection dbConnection = await GetDbConnectionAsync())
                return await method(dbConnection);
        }

        protected void WithConnection(Action<IDbConnection> method)
        {
            using (IDbConnection dbConnection = GetDbConnection())
                method(dbConnection);
        }

        protected async Task WithConnectionAsync(Func<IDbConnection, Task> method)
        {
            using (IDbConnection dbConnection = await GetDbConnectionAsync())
                await method(dbConnection);
        }

        protected T WithTransaction<T>(Func<IDbConnection, IDbTransaction, T> method)
        {
            return WithConnection<T>((connection =>
            {
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        return method(connection, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                    }
                }
                return default(T);
            }));
        }

        protected async Task<T> WithTransactionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> method)
        {
            return await WithConnectionAsync<T>(connection =>
            {
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        return method(connection, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                    }
                }

                return Task.FromResult(default(T));
            });
        }

        protected void WithTransaction(Action<IDbConnection, IDbTransaction> method)
        {
            WithConnection(connection =>
            {
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        method(connection, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            });
        }

        protected async Task WithTransactionAsync(Func<IDbConnection, IDbTransaction, Task> method)
        {
            await WithConnectionAsync(async connection =>
            {
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        await method(connection, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            });
        }

        protected int Count<T>(object predicate = null) where T : class
        {
            return WithConnection(c => c.Count<T>(predicate));
        }

        protected async Task<int> CountAsync<T>(object predicate = null) where T : class
        {
            return await WithConnectionAsync(c => c.CountAsync<T>(predicate));
        }

        protected int Execute(string sql, object param = null)
        {
            return WithConnection(c => c.Execute(sql, param));
        }

        protected async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await WithConnectionAsync(c => c.ExecuteAsync(sql, param));
        }

        protected IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return WithConnection(c => c.Query<T>(sql, param));
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return await WithConnectionAsync(c => c.QueryAsync<T>(sql, param));
        }

        protected T QueryOne<T>(string sql, object param = null)
        {
            var x = WithConnection(c => c.Query<T>(sql, param));

            return x.FirstOrDefault();
        }

        protected async Task<T> QueryOneAsync<T>(string sql, object param = null)
        {
            return (await WithConnectionAsync(c => c.QueryAsync<T>(sql, param))).FirstOrDefault();
        }
    }
}
