using Rainbow.DapperExtensions.Mapper;
using Rainbow.DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Rainbow.DapperExtensions
{
    public static class DapperExtensions
    {
        private readonly static object _lock = new object();

        private static Func<IDapperExtensionsConfiguration, IDapperImplementor> _instanceFactory;
        private static IDapperImplementor _instance;
        private static IDapperExtensionsConfiguration _configuration;

        /// <summary>
        /// Gets or sets the default class mapper to use when generating class maps. If not specified, AutoClassMapper<T> is used.
        /// DapperExtensions.Configure(Type, IList<Assembly>, ISqlDialect) can be used instead to set all values at once
        /// </summary>
        public static Type DefaultMapper
        {
            get
            {
                return _configuration.DefaultMapper;
            }

            set
            {
                Configure(value, _configuration.MappingAssemblies, _configuration.Dialect);
            }
        }

        /// <summary>
        /// Gets or sets the type of sql to be generated.
        /// DapperExtensions.Configure(Type, IList<Assembly>, ISqlDialect) can be used instead to set all values at once
        /// </summary>
        public static ISqlDialect SqlDialect
        {
            get
            {
                return _configuration.Dialect;
            }

            set
            {
                Configure(_configuration.DefaultMapper, _configuration.MappingAssemblies, value);
            }
        }

        /// <summary>
        /// Get or sets the Dapper Extensions Implementation Factory.
        /// </summary>
        public static Func<IDapperExtensionsConfiguration, IDapperImplementor> InstanceFactory
        {
            get
            {
                if (_instanceFactory == null)
                {
                    _instanceFactory = config => new DapperImplementor(new SqlGeneratorImpl(config));
                }

                return _instanceFactory;
            }
            set
            {
                _instanceFactory = value;
                Configure(_configuration.DefaultMapper, _configuration.MappingAssemblies, _configuration.Dialect);
            }
        }

        /// <summary>
        /// Gets the Dapper Extensions Implementation
        /// </summary>
        private static IDapperImplementor Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = InstanceFactory(_configuration);
                        }
                    }
                }

                return _instance;
            }
        }

        static DapperExtensions()
        {
            Configure(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
        }

        /// <summary>
        /// Add other assemblies that Dapper Extensions will search if a mapping is not found in the same assembly of the POCO.
        /// </summary>
        /// <param name="assemblies"></param>
        public static void SetMappingAssemblies(IList<Assembly> assemblies)
        {
            Configure(_configuration.DefaultMapper, assemblies, _configuration.Dialect);
        }

        /// <summary>
        /// Configure DapperExtensions extension methods.
        /// </summary>
        /// <param name="defaultMapper"></param>
        /// <param name="mappingAssemblies"></param>
        /// <param name="sqlDialect"></param>
        public static void Configure(IDapperExtensionsConfiguration configuration)
        {
            _instance = null;
            _configuration = configuration;
        }

        /// <summary>
        /// Configure DapperExtensions extension methods.
        /// </summary>
        /// <param name="defaultMapper"></param>
        /// <param name="mappingAssemblies"></param>
        /// <param name="sqlDialect"></param>
        public static void Configure(Type defaultMapper, IList<Assembly> mappingAssemblies, ISqlDialect sqlDialect)
        {
            Configure(new DapperExtensionsConfiguration(defaultMapper, mappingAssemblies, sqlDialect));
        }

        #region Get

        /// <summary>
        /// Executes a query for the specified id, returning the data typed as per T
        /// </summary>
        public static T Get<T>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var result = Instance.Get<T>(connection, id, transaction, commandTimeout);
            return (T)result;
        }

        /// <summary>
        /// Asynchronous executes a query for the specified id, returning the data typed as per T
        /// </summary>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var result = await Instance.GetAsync<T>(connection, id, transaction, commandTimeout);
            return (T)result;
        }

        /// <summary>
        /// Executes a query for the specified expression, returning the data typed as per T
        /// </summary>
        public static T Get<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var result = Instance.Get<T>(connection, expression, sort, transaction, commandTimeout);
            return (T)result;
        }

        /// <summary>
        /// Asynchronous executes  a query for the specified expression, returning the data typed as per T
        /// </summary>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var result = await Instance.GetAsync<T>(connection, expression, sort, transaction, commandTimeout);
            return (T)result;
        }

        /// <summary>
        /// Executes  a query for the specified expression, returning the data typed as per T
        /// </summary>
        public static T Get<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var result = Instance.Get<T>(connection, properties, expression, sort, transaction, commandTimeout);
            return (T)result;
        }

        /// <summary>
        /// Asynchronous executes  a query for the specified expression, returning the data typed as per T
        /// </summary>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var result = await Instance.GetAsync<T>(connection, properties, expression, sort, transaction, commandTimeout);
            return (T)result;
        }


        #endregion

        #region Insert
        /// <summary>
        /// Executes an insert query for the specified entity.
        /// </summary>
        public static void Insert<T>(this IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Instance.Insert<T>(connection, entities, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes an insert query for the specified entity.
        /// </summary>
        public static async Task InsertAsync<T>(this IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            await Instance.InsertAsync<T>(connection, entities, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes an insert query for the specified entity, returning the primary key.  
        /// If the entity has a single key, just the value is returned.  
        /// If the entity has a composite key, an IDictionary&lt;string, object&gt; is returned with the key values.
        /// The key value for the entity will also be updated if the KeyType is a Guid or Identity.
        /// </summary>
        public static dynamic Insert<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return Instance.Insert<T>(connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes an insert query for the specified entity, returning the primary key.  
        /// If the entity has a single key, just the value is returned.  
        /// If the entity has a composite key, an IDictionary&lt;string, object&gt; is returned with the key values.
        /// The key value for the entity will also be updated if the KeyType is a Guid or Identity.
        /// </summary>
        public static async Task<dynamic> InsertAsync<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await Instance.InsertAsync<T>(connection, entity, transaction, commandTimeout);
        }

        #endregion

        #region Update
        /// <summary>
        /// Executes an update query for the specified entity.
        /// </summary>
        public static bool Update<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null, bool ignoreAllKeyProperties = false) where T : class
        {
            return Instance.Update<T>(connection, entity, transaction, commandTimeout, ignoreAllKeyProperties);
        }

        /// <summary>
        /// Asynchronous executes an update query for the specified entity.
        /// </summary>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class
        {
            return await Instance.UpdateAsync<T>(connection, entity, transaction, commandTimeout, ignoreAllKeyProperties);
        }

        /// <summary>
        /// Executes an update query for the specified expression predicate.(Support partial updates)
        /// </summary>
        public static bool Update<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class
        {
            return Instance.Update<T>(connection, properties, expression, transaction, commandTimeout, ignoreAllKeyProperties);
        }

        /// <summary>
        /// Asynchronous Executes an update query for the specified expression predicate.(Support partial updates)
        /// </summary>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class
        {
            return await Instance.UpdateAsync<T>(connection, properties, expression, transaction, commandTimeout, ignoreAllKeyProperties);
        }

        #endregion

        #region Delete
        /// <summary>
        /// Executes a delete query for the specified entity.
        /// </summary>
        public static bool Delete<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return Instance.Delete<T>(connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes a delete query for the specified entity.
        /// </summary>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await Instance.DeleteAsync<T>(connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a delete query using the specified predicate.
        /// </summary>
        public static bool Delete<T>(this IDbConnection connection, object predicate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return Instance.Delete<T>(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes a delete query using the specified predicate.
        /// </summary>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, object predicate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await Instance.DeleteAsync<T>(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a delete query using the specified expression predicate.
        /// </summary>
        public static bool Delete<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Instance.Delete<T>(connection, expression, transaction, commandTimeout);
        }

        /// <summary>
        ///Asynchronous executes a delete query using the specified expression predicate.
        /// </summary>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.DeleteAsync<T>(connection, expression, transaction, commandTimeout);
        }
        #endregion

        #region GetList
        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false) where T : class
        {
            return Instance.GetList<T>(connection, predicate, sort, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetListAsync<T>(connection, predicate, sort, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetList<T>(connection, properties, predicate, sort, transaction, commandTimeout, buffered);
        }
        /// <summary>
        /// Asynchronous executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetListAsync<T>(connection, properties, predicate, sort, transaction, commandTimeout);
        }
        /// <summary>
        /// Executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetList<T>(connection, properties, expression, sort, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// </summary>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetListAsync<T>(connection, properties, expression, sort, transaction, commandTimeout);
        }

        #endregion

        #region GetPage
        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static IEnumerable<T> GetPage<T>(this IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false) where T : class
        {
            return Instance.GetPage<T>(connection, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static async Task<IEnumerable<T>> GetPageAsync<T>(this IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetPageAsync<T>(connection, predicate, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static IEnumerable<T> GetPage<T>(this IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetPage<T>(connection, properties, predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static async Task<IEnumerable<T>> GetPageAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetPageAsync<T>(connection, properties, predicate, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static IEnumerable<T> GetPage<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetPage<T>(connection, properties, expression, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified page and resultsPerPage.
        /// </summary>
        public static async Task<IEnumerable<T>> GetPageAsync<T>(this IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetPageAsync<T>(connection, properties, expression, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        #endregion

        #region GetSet
        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static IEnumerable<T> GetSet<T>(this IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false) where T : class
        {
            return Instance.GetSet<T>(connection, predicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static async Task<IEnumerable<T>> GetSetAsync<T>(this IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetSetAsync<T>(connection, predicate, sort, firstResult, maxResults, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static IEnumerable<T> GetSet<T>(this IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetSet<T>(connection, properties, predicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the specified predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetSetAsync<T>(connection, properties, predicate, sort, firstResult, maxResults, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static IEnumerable<T> GetSet<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return Instance.GetSet<T>(connection, properties, expression, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// Asynchronous executes a select query using the expression predicate, returning an IEnumerable data typed as per T.
        /// Data returned is dependent upon the specified firstResult and maxResults.
        /// </summary>
        public static async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.GetSetAsync<T>(connection, properties, expression, sort, firstResult, maxResults, transaction, commandTimeout);
        }
        #endregion

        #region Count
        /// <summary>
        /// Executes a query using the specified predicate, returning an integer that represents the number of rows that match the query.
        /// </summary>
        public static int Count<T>(this IDbConnection connection, object predicate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return Instance.Count<T>(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes a query using the specified predicate, returning an integer that represents the number of rows that match the query.
        /// </summary>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.CountAsync<T>(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// Executes a query using the expression predicate, returning an integer that represents the number of rows that match the query.
        /// </summary>
        public static int Count<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return Instance.Count<T>(connection, expression, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes a query using the expression predicate, returning an integer that represents the number of rows that match the query.
        /// </summary>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return await Instance.CountAsync<T>(connection, expression, transaction, commandTimeout);
        }
        #endregion

        #region GetMultiple
        /// <summary>
        /// Executes a select query for multiple objects, returning IMultipleResultReader for each predicate.
        /// </summary>
        public static IMultipleResultReader GetMultiple(this IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Instance.GetMultiple(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// Asynchronous executes a select query for multiple objects, returning IMultipleResultReader for each predicate.
        /// </summary>
        public static async Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            return await Instance.GetMultipleAsync(connection, predicate, transaction, commandTimeout);
        }
        #endregion


        /// <summary>
        /// Gets the appropriate mapper for the specified type T. 
        /// If the mapper for the type is not yet created, a new mapper is generated from the mapper type specifed by DefaultMapper.
        /// </summary>
        public static IClassMapper GetMap<T>() where T : class
        {
            return Instance.SqlGenerator.Configuration.GetMap<T>();
        }

        /// <summary>
        /// Clears the ClassMappers for each type.
        /// </summary>
        public static void ClearCache()
        {
            Instance.SqlGenerator.Configuration.ClearCache();
        }

        /// <summary>
        /// Generates a COMB Guid which solves the fragmented index issue.
        /// See: http://davybrion.com/blog/2009/05/using-the-guidcomb-identifier-strategy
        /// </summary>
        public static Guid GetNextGuid()
        {
            return Instance.SqlGenerator.Configuration.GetNextGuid();
        }
    }
}
