using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rainbow.DapperExtensions.Repository
{
    public interface IEntityRepository
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();

        dynamic Add<T>(T entity) where T : class;
        Task<dynamic> AddAsync<T>(T entity) where T : class;
        void Add<T>(IEnumerable<T> entities) where T : class;
        Task AddAsync<T>(IEnumerable<T> entities) where T : class;

        bool Update<T>(T entity) where T : class;
        Task<bool> UpdateAsync<T>(T entity) where T : class;
        bool Update<T>(Expression<Func<T, object>> properties, Expression<Func<T, bool>> predicate) where T : class;
        Task<bool> UpdateAsync<T>(Expression<Func<T, object>> properties, Expression<Func<T, bool>> predicate) where T : class;

        bool Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Get<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null) where T : class;
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null) where T : class;
        T Get<T>(string sql, object param = null) where T : class;
        Task<T> GetAsync<T>(string sql, object param = null) where T : class;

        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null) where T : class;
        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null, int pageIndex = 1, int pageSize = 10) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> properties = null, IList<ISort> sort = null, int pageIndex = 1, int pageSize = 10) where T : class;

        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        IMultipleResultReader QueryMultiple(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);
        Task<IMultipleResultReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        IDataReader QueryDataReader(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);
        Task<IDataReader> QueryDataReaderAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        int Execute(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        Guid GetNextGuid();
    }
}
