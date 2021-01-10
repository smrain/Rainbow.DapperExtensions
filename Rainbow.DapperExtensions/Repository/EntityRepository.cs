using Dapper;
using Rainbow.DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rainbow.DapperExtensions.Repository
{
    public abstract class EntityRepository : IEntityRepository, IDisposable
    {
        #region Properties 
        protected IDbConnection Connection { get; private set; }
        protected abstract IDbConnection GetConnection();
        protected virtual int CommandTimeout { get; set; } = 15;

        protected bool HasActiveTransaction { get { return _currentTransaction != null; } }
        protected Guid TransactionId { get; private set; }

        private readonly string _connectionString;

        private static bool _isdisposed = false;

        private static IDbTransaction _currentTransaction = null;

        private static readonly IDictionary<string, ISqlDialect> SqlDialectDictionary
            = new Dictionary<string, ISqlDialect>(7)
            {
                ["sqlconnection"] = new SqlServerDialect(),
                ["mysqlconnection"] = new MySqlDialect(),
                ["npgsqlconnection"] = new PostgreSqlDialect(),
                ["oracleconnection"] = new OracleDialect(),
                ["sqlceconnection"] = new SqlCeDialect(),
                ["sqliteconnection"] = new SqliteDialect(),
                ["oledbconnection"] = new DB2Dialect()
            };

        #endregion

        #region Ctor
        protected EntityRepository(string connectionString) : this((IDbConnection)null)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                _connectionString = connectionString;
                Connection.ConnectionString = _connectionString;
            }
        }

        protected EntityRepository(IDbConnection connection)
        {
            Connection = connection ?? GetConnection();
            if (Connection == null) throw new ArgumentNullException(nameof(Connection));

            DapperExtensions.SqlDialect = GetSqlDialect(Connection);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isdisposed)
            {
                if (disposing)
                {
                    if (Connection?.State != ConnectionState.Closed)
                    {
                        _currentTransaction?.Dispose();
                        TransactionId = Guid.Empty;
                        _currentTransaction = null;

                        Connection?.Close();
                        Connection?.Dispose();
                    }
                    _isdisposed = true;
                }
            }
        }

        ~EntityRepository()
        {
            Dispose(false);
        }

        #endregion

        #region Transaction
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Connection.State != ConnectionState.Open) { Connection.Open(); }

            if (!HasActiveTransaction)
            {
                _currentTransaction = Connection.BeginTransaction(isolationLevel);
                TransactionId = Guid.NewGuid();
            }
        }
        public void Commit()
        {
            _currentTransaction?.Commit();
            _currentTransaction = null;
            TransactionId = Guid.Empty;
        }
        public void Rollback()
        {
            _currentTransaction?.Rollback();
            _currentTransaction = null;
            TransactionId = Guid.Empty;
        }
        #endregion

        #region Operation
        public virtual dynamic Add<T>(T entity) where T : class
            => Execute(() => Connection.Insert(entity, _currentTransaction, CommandTimeout), SqlType.DML);
        public virtual async Task<dynamic> AddAsync<T>(T entity) where T : class
            => await ExecuteAsync(() => Connection.InsertAsync(entity, _currentTransaction, CommandTimeout), SqlType.DML);

        public virtual void Add<T>(IEnumerable<T> entities) where T : class
            => Execute(() => Connection.Insert(entities, _currentTransaction, CommandTimeout), SqlType.DML);
        public virtual async Task AddAsync<T>(IEnumerable<T> entities) where T : class
            => await ExecuteAsync(() => Connection.InsertAsync(entities, _currentTransaction, CommandTimeout), SqlType.DML);

        public virtual bool Update<T>(T entity) where T : class
            => Execute(() => Connection.Update(entity, _currentTransaction, CommandTimeout), SqlType.DML);
        public virtual async Task<bool> UpdateAsync<T>(T entity) where T : class
            => await ExecuteAsync(() => Connection.UpdateAsync(entity, _currentTransaction, CommandTimeout, true), SqlType.DML);
        public virtual bool Update<T>(Expression<Func<T, object>> properties, Expression<Func<T, bool>> predicate) where T : class
            => Execute(() => Connection.Update(properties, predicate, _currentTransaction, CommandTimeout, false), SqlType.DML);
        public virtual async Task<bool> UpdateAsync<T>(Expression<Func<T, object>> properties, Expression<Func<T, bool>> predicate) where T : class
            => await ExecuteAsync(() => Connection.UpdateAsync(properties, predicate, _currentTransaction, CommandTimeout, false), SqlType.DML);

        public virtual bool Delete<T>(Expression<Func<T, bool>> predicate) where T : class
            => Execute(() => Connection.Delete(predicate, _currentTransaction, CommandTimeout), SqlType.DML);
        public virtual async Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
            => await ExecuteAsync(() => Connection.DeleteAsync(predicate, _currentTransaction, CommandTimeout), SqlType.DML);

        public virtual T Get<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null) where T : class
            => Execute(() => Connection.Get(properties, predicate, null, _currentTransaction, CommandTimeout), SqlType.DQL);
        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null) where T : class
            => await ExecuteAsync(() => Connection.GetAsync(properties, predicate, null, _currentTransaction, CommandTimeout), SqlType.DQL);
        public T Get<T>(string sql, object param = null) where T : class
            => Execute(() => Connection.QueryFirstOrDefault<T>(sql, param, _currentTransaction, CommandTimeout), SqlType.DQL);
        public async Task<T> GetAsync<T>(string sql, object param = null) where T : class
            => await ExecuteAsync(() => Connection.QuerySingleOrDefaultAsync<T>(sql, param, _currentTransaction, CommandTimeout), SqlType.DQL);

        public virtual IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null) where T : class
            => Execute(() => Connection.GetList(properties, predicate, sort, _currentTransaction, CommandTimeout, true), SqlType.DQL);
        public virtual async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null) where T : class
            => await ExecuteAsync(() => Connection.GetListAsync(properties, predicate, sort, _currentTransaction, CommandTimeout), SqlType.DQL);

        public virtual IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null, int pageIndex = 1, int pageSize = 10) where T : class
            => Execute(() => Connection.GetPage(properties, predicate, sort, pageIndex - 1, pageSize, _currentTransaction, CommandTimeout, true), SqlType.DQL);
        public virtual async Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null, int pageIndex = 1, int pageSize = 10) where T : class
            => await ExecuteAsync(() => Connection.GetPageAsync(properties, predicate, sort, pageIndex - 1, pageSize, _currentTransaction, CommandTimeout), SqlType.DQL);

        public virtual int Count<T>(Expression<Func<T, bool>> predicate) where T : class
            => Execute(() => Connection.Count(predicate, _currentTransaction, CommandTimeout), SqlType.DQL);
        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class
            => await ExecuteAsync(() => Connection.CountAsync(predicate, _currentTransaction, CommandTimeout), SqlType.DQL);

        public virtual IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => Execute(() => Connection.Query<T>(sql, param, _currentTransaction, true, commandTimeout, commandType), SqlType.DQL);
        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => await ExecuteAsync(() => Connection.QueryAsync<T>(sql, param, _currentTransaction, commandTimeout, commandType), SqlType.DQL);

        public virtual IMultipleResultReader QueryMultiple(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => Execute(() => new GridReaderResultReader(Connection.QueryMultiple(sql, param, _currentTransaction, commandTimeout, commandType)), SqlType.DQL);
        public virtual async Task<IMultipleResultReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => await ExecuteAsync(async () => new GridReaderResultReader(await Connection.QueryMultipleAsync(sql, param, _currentTransaction, commandTimeout, commandType)), SqlType.DQL);

        public virtual IDataReader QueryDataReader(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => Execute(() => Connection.ExecuteReader(sql, param, _currentTransaction, commandTimeout, commandType), SqlType.DQL);
        public virtual async Task<IDataReader> QueryDataReaderAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => await ExecuteAsync(() => Connection.ExecuteReaderAsync(sql, param, _currentTransaction, commandTimeout, commandType), SqlType.DQL);

        public virtual int Execute(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => Execute(() => Connection.Execute(sql, param, _currentTransaction, commandTimeout, commandType), SqlType.DML);
        public virtual async Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null)
            => await ExecuteAsync(() => Connection.ExecuteAsync(sql, param, _currentTransaction, commandTimeout, commandType), SqlType.DML);

        public virtual Guid GetNextGuid()
        {
            return DapperExtensions.GetNextGuid();
        }

        #endregion

        #region Basic Mothod
        protected virtual void TryOpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                if (_isdisposed) Connection = GetConnection();
                Connection.Open();
            }
        }

        protected virtual TResult Execute<TResult>(Func<TResult> fun, SqlType sqlType = SqlType.Unknown)
        {
            TResult result = default;
            if (Connection != null && fun != null)
            {
                try
                {
                    TryOpenConnection();

                    result = fun();
                }
                catch (Exception ex)
                {
                    Rollback();
                    throw ex;
                }
                finally
                {
                    if (!HasActiveTransaction && !(result is IMultipleResultReader || result is IDataReader))
                    {
                        Dispose();
                    }
                }
            }
            return result;
        }
        protected virtual void Execute(Action action, SqlType sqlType = SqlType.Unknown)
        {
            if (Connection != null && action != null)
            {
                try
                {
                    TryOpenConnection();

                    action();
                }
                catch (Exception ex)
                {
                    Rollback();
                    throw ex;
                }
                finally
                {
                    if (!HasActiveTransaction)
                    {
                        Dispose();
                    }
                }
            }
        }

        protected virtual async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> fun, SqlType sqlType = SqlType.Unknown)
        {
            TResult result = default;
            if (Connection != null && fun != null)
            {
                try
                {
                    TryOpenConnection();

                    result = await fun();
                }
                catch (Exception ex)
                {
                    Rollback();
                    throw ex;
                }
                finally
                {
                    if (!HasActiveTransaction && !(result is IMultipleResultReader || result is IDataReader))
                    {
                        Dispose();
                    }
                }
            }
            return result;
        }
        protected virtual async Task ExecuteAsync(Func<Task> action, SqlType sqlType = SqlType.Unknown)
        {
            if (Connection != null && action != null)
            {
                try
                {
                    TryOpenConnection();

                    await action();
                }
                catch (Exception ex)
                {
                    Rollback();
                    throw ex;
                }
                finally
                {
                    if (!HasActiveTransaction)
                    {
                        Dispose();
                    }
                }
            }
        }

        private static ISqlDialect GetSqlDialect(IDbConnection connection)
        {
            var name = connection?.GetType().Name.ToLower();
            return SqlDialectDictionary.ContainsKey(name) ? SqlDialectDictionary[name] :
                throw new NotSupportedException($"Not Support DbConnection of {name}");
        }

        protected enum SqlType
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Data Query Language
            /// </summary>
            DQL = 1,

            /// <summary>
            /// Data Manipulation Language
            /// </summary>
            DML = 2,

            /// <summary>
            /// Data Definition Language
            /// </summary>
            DDL = 3,

            /// <summary>
            /// Data Control Language
            /// </summary>
            DCL = 4
        }

        #endregion
    }
}
