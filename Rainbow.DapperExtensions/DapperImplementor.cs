using Dapper;
using Rainbow.DapperExtensions.Mapper;
using Rainbow.DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.DapperExtensions
{
    public interface IDapperImplementor
    {
        ISqlGenerator SqlGenerator { get; }

        T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<T> GetAsync<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<T> GetAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;
        T Get<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<T> GetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;

        dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<dynamic> InsertAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task InsertAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class;

        bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class;
        Task<bool> UpdateAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class;
        bool Update<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class;
        Task<bool> UpdateAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties) where T : class;

        bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class;
        bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        bool Delete<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<bool> DeleteAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class;

        IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetList<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetList<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class;

        IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetPage<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetPage<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class;

        IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetSet<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class;
        IEnumerable<T> GetSet<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;
        Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class;

        int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<int> CountAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class;
        int Count<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class;
        Task<int> CountAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class;

        IMultipleResultReader GetMultiple(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout);
        Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout);
    }

    public class DapperImplementor : IDapperImplementor
    {
        public DapperImplementor(ISqlGenerator sqlGenerator)
        {
            SqlGenerator = sqlGenerator;

            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }

        // Map string to guid in mysql
        public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
        {
            public override void SetValue(IDbDataParameter parameter, Guid guid)
            {
                parameter.Value = guid.ToString();
            }

            public override Guid Parse(object value)
            {
                return new Guid((string)value);
            }
        }

        public ISqlGenerator SqlGenerator { get; private set; }

        private void FillSelectedProperties<T>(IClassMapper classMap, Expression<Func<T, object>> properties) where T : class
        {
            if (properties != null)
            {
                var select_columns = ReflectionHelper.GetObjectValues(properties);
                if (select_columns != null && select_columns.Any())
                {
                    var all_columns = classMap.Properties.Where(p => !(p.Ignored));
                    var filtered_columns = all_columns.Where(property => select_columns.Any(c => c.Key.Equals(property.ColumnName, StringComparison.OrdinalIgnoreCase)))?.ToList();
                    if (filtered_columns == null || !filtered_columns.Any()) throw new Exception("No columns were mapped.");
                    classMap.SelectedProperties = filtered_columns;
                }
            }
        }


        #region Get DapperImplementor

        public T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetIdPredicate(classMap, id);
            T result = Get<T>(connection, classMap, predicate, null, transaction, commandTimeout);
            return result;
        }

        public async Task<T> GetAsync<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetIdPredicate(classMap, id);
            T result = await GetAsync<T>(connection, classMap, predicate, null, transaction, commandTimeout);
            return result;
        }

        public T Get<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetExpressionPredicate(classMap, expression);
            T result = Get<T>(connection, classMap, predicate, sort, transaction, commandTimeout);
            return result;
        }

        public async Task<T> GetAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetExpressionPredicate(classMap, expression);
            T result = await GetAsync<T>(connection, classMap, predicate, sort, transaction, commandTimeout);
            return result;
        }

        public T Get<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);
            IPredicate predicate = GetExpressionPredicate(classMap, expression);
            T result = Get<T>(connection, classMap, predicate, sort, transaction, commandTimeout);
            return result;
        }

        public async Task<T> GetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);
            IPredicate predicate = GetExpressionPredicate(classMap, expression);
            T result = await GetAsync<T>(connection, classMap, predicate, sort, transaction, commandTimeout);
            return result;
        }


        protected T Get<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.QueryFirstOrDefault<T>(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
        }

        protected async Task<T> GetAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.QueryFirstOrDefaultAsync<T>(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
        }

        #endregion

        #region Insert DapperImplementor

        public void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IEnumerable<PropertyInfo> properties = null;
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var notKeyProperties = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            var triggerIdentityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            var parameters = new List<DynamicParameters>();
            if (triggerIdentityColumn != null)
            {
                properties = typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.Name != triggerIdentityColumn.PropertyInfo.Name);
            }

            foreach (var e in entities)
            {
                foreach (var column in notKeyProperties)
                {
                    if (column.KeyType == KeyType.Guid && (Guid)column.PropertyInfo.GetValue(e, null) == Guid.Empty)
                    {
                        Guid comb = SqlGenerator.Configuration.GetNextGuid();
                        column.PropertyInfo.SetValue(e, comb, null);
                    }
                }

                if (triggerIdentityColumn != null)
                {
                    var dynamicParameters = new DynamicParameters();
                    foreach (var prop in properties)
                    {
                        dynamicParameters.Add(prop.Name, prop.GetValue(e, null));
                    }

                    // defaultValue need for identify type of parameter
                    var defaultValue = typeof(T).GetProperty(triggerIdentityColumn.PropertyInfo.Name).GetValue(e, null);
                    dynamicParameters.Add("IdOutParam", direction: ParameterDirection.Output, value: defaultValue);

                    parameters.Add(dynamicParameters);
                }
            }

            string sql = SqlGenerator.Insert(classMap);

            if (triggerIdentityColumn == null)
            {
                connection.Execute(sql, entities, transaction, commandTimeout, CommandType.Text);
            }
            else
            {
                connection.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
            }
        }

        public async Task InsertAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IEnumerable<PropertyInfo> properties = null;
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            var notKeyProperties = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            var triggerIdentityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            var parameters = new List<DynamicParameters>();
            if (triggerIdentityColumn != null)
            {
                properties = typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.Name != triggerIdentityColumn.PropertyInfo.Name);
            }

            foreach (var e in entities)
            {
                foreach (var column in notKeyProperties)
                {
                    if (column.KeyType == KeyType.Guid && (Guid)column.PropertyInfo.GetValue(e, null) == Guid.Empty)
                    {
                        Guid comb = SqlGenerator.Configuration.GetNextGuid();
                        column.PropertyInfo.SetValue(e, comb, null);
                    }
                }

                if (triggerIdentityColumn != null)
                {
                    var dynamicParameters = new DynamicParameters();
                    foreach (var prop in properties)
                    {
                        dynamicParameters.Add(prop.Name, prop.GetValue(e, null));
                    }

                    // defaultValue need for identify type of parameter
                    var defaultValue = typeof(T).GetProperty(triggerIdentityColumn.PropertyInfo.Name).GetValue(e, null);
                    dynamicParameters.Add("IdOutParam", direction: ParameterDirection.Output, value: defaultValue);

                    parameters.Add(dynamicParameters);
                }
            }

            string sql = SqlGenerator.Insert(classMap);

            if (triggerIdentityColumn == null)
            {
                await connection.ExecuteAsync(sql, entities, transaction, commandTimeout, CommandType.Text);
            }
            else
            {
                await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout, CommandType.Text);
            }
        }

        public dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            List<IPropertyMap> nonIdentityKeyProperties = classMap.Properties.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.Identity);
            var triggerIdentityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);
            foreach (var column in nonIdentityKeyProperties)
            {
                if (column.KeyType == KeyType.Guid && (Guid)column.PropertyInfo.GetValue(entity, null) == Guid.Empty)
                {
                    Guid comb = SqlGenerator.Configuration.GetNextGuid();
                    column.PropertyInfo.SetValue(entity, comb, null);
                }
            }

            IDictionary<string, object> keyValues = new ExpandoObject();
            string sql = SqlGenerator.Insert(classMap);
            if (identityColumn != null)
            {
                IEnumerable<long> result;
                if (SqlGenerator.SupportsMultipleStatements())
                {
                    sql += SqlGenerator.Configuration.Dialect.BatchSeperator + SqlGenerator.IdentitySql(classMap);
                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }
                else
                {
                    connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
                    sql = SqlGenerator.IdentitySql(classMap);
                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }

                // We are only interested in the first identity, but we are iterating over all resulting items (if any).
                // This makes sure that ADO.NET drivers (like MySql) won't actively terminate the query.
                bool hasResult = false;
                int identityInt = 0;
                foreach (var identityValue in result)
                {
                    if (hasResult)
                    {
                        continue;
                    }
                    identityInt = Convert.ToInt32(identityValue);
                    hasResult = true;
                }
                if (!hasResult)
                {
                    throw new InvalidOperationException("The source sequence is empty.");
                }

                keyValues.Add(identityColumn.Name, identityInt);
                identityColumn.PropertyInfo.SetValue(entity, identityInt, null);
            }
            else if (triggerIdentityColumn != null)
            {
                var dynamicParameters = new DynamicParameters();
                foreach (var prop in entity.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.Name != triggerIdentityColumn.PropertyInfo.Name))
                {
                    dynamicParameters.Add(prop.Name, prop.GetValue(entity, null));
                }

                // defaultValue need for identify type of parameter
                var defaultValue = entity.GetType().GetProperty(triggerIdentityColumn.PropertyInfo.Name).GetValue(entity, null);
                dynamicParameters.Add("IdOutParam", direction: ParameterDirection.Output, value: defaultValue);

                connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);

                var value = dynamicParameters.Get<object>(SqlGenerator.Configuration.Dialect.ParameterPrefix + "IdOutParam");
                keyValues.Add(triggerIdentityColumn.Name, value);
                triggerIdentityColumn.PropertyInfo.SetValue(entity, value, null);
            }
            else
            {
                connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
            }

            foreach (var column in nonIdentityKeyProperties)
            {
                keyValues.Add(column.Name, column.PropertyInfo.GetValue(entity, null));
            }

            if (keyValues.Count == 1)
            {
                return keyValues.First().Value;
            }

            return keyValues;
        }

        public async Task<dynamic> InsertAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            List<IPropertyMap> nonIdentityKeyProperties = classMap.Properties.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.Identity);
            var triggerIdentityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);
            foreach (var column in nonIdentityKeyProperties)
            {
                if (column.KeyType == KeyType.Guid && (Guid)column.PropertyInfo.GetValue(entity, null) == Guid.Empty)
                {
                    Guid comb = SqlGenerator.Configuration.GetNextGuid();
                    column.PropertyInfo.SetValue(entity, comb, null);
                }
            }

            IDictionary<string, object> keyValues = new ExpandoObject();
            string sql = SqlGenerator.Insert(classMap);
            if (identityColumn != null)
            {
                IEnumerable<long> result;
                if (SqlGenerator.SupportsMultipleStatements())
                {
                    sql += SqlGenerator.Configuration.Dialect.BatchSeperator + SqlGenerator.IdentitySql(classMap);
                    result = await connection.QueryAsync<long>(sql, entity, transaction, commandTimeout, CommandType.Text);
                }
                else
                {
                    connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
                    sql = SqlGenerator.IdentitySql(classMap);
                    result = await connection.QueryAsync<long>(sql, entity, transaction, commandTimeout, CommandType.Text);
                }

                // We are only interested in the first identity, but we are iterating over all resulting items (if any).
                // This makes sure that ADO.NET drivers (like MySql) won't actively terminate the query.
                bool hasResult = false;
                int identityInt = 0;
                foreach (var identityValue in result)
                {
                    if (hasResult)
                    {
                        continue;
                    }
                    identityInt = Convert.ToInt32(identityValue);
                    hasResult = true;
                }
                if (!hasResult)
                {
                    throw new InvalidOperationException("The source sequence is empty.");
                }

                keyValues.Add(identityColumn.Name, identityInt);
                identityColumn.PropertyInfo.SetValue(entity, identityInt, null);
            }
            else if (triggerIdentityColumn != null)
            {
                var dynamicParameters = new DynamicParameters();
                foreach (var prop in entity.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.Name != triggerIdentityColumn.PropertyInfo.Name))
                {
                    dynamicParameters.Add(prop.Name, prop.GetValue(entity, null));
                }

                // defaultValue need for identify type of parameter
                var defaultValue = entity.GetType().GetProperty(triggerIdentityColumn.PropertyInfo.Name).GetValue(entity, null);
                dynamicParameters.Add("IdOutParam", direction: ParameterDirection.Output, value: defaultValue);

                await connection.ExecuteAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);

                var value = dynamicParameters.Get<object>(SqlGenerator.Configuration.Dialect.ParameterPrefix + "IdOutParam");
                keyValues.Add(triggerIdentityColumn.Name, value);
                triggerIdentityColumn.PropertyInfo.SetValue(entity, value, null);
            }
            else
            {
                await connection.ExecuteAsync(sql, entity, transaction, commandTimeout, CommandType.Text);
            }

            foreach (var column in nonIdentityKeyProperties)
            {
                keyValues.Add(column.Name, column.PropertyInfo.GetValue(entity, null));
            }

            if (keyValues.Count == 1)
            {
                return keyValues.First().Value;
            }

            return keyValues;
        }

        #endregion

        #region Update DapperImplementor

        public bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties = false) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();

            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Update(classMap, predicate, parameters, ignoreAllKeyProperties);
            DynamicParameters dynamicParameters = new DynamicParameters();

            var columns = ignoreAllKeyProperties
                ? classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                : classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity || p.KeyType == KeyType.Assigned));

            foreach (var property in ReflectionHelper.GetObjectValues(entity).Where(property => columns.Any(c => c.Name == property.Key)))
            {
                dynamicParameters.Add(property.Key, property.Value);
            }

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        public async Task<bool> UpdateAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties = false) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();

            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Update(classMap, predicate, parameters, ignoreAllKeyProperties);
            DynamicParameters dynamicParameters = new DynamicParameters();

            var columns = ignoreAllKeyProperties
                ? classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                : classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity || p.KeyType == KeyType.Assigned));

            foreach (var property in ReflectionHelper.GetObjectValues(entity).Where(property => columns.Any(c => c.Name == property.Key)))
            {
                dynamicParameters.Add(property.Key, property.Value);
            }

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.ExecuteAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        public bool Update<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties = false) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();

            DynamicParameters dynamicParameters = new DynamicParameters();

            if (properties == null) throw new ArgumentNullException(nameof(properties));

            var set_columns = ReflectionHelper.GetObjectValues(properties);
            if (set_columns != null && set_columns.Any())
            {
                var columns = ignoreAllKeyProperties
                    ? classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                    : classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity || p.KeyType == KeyType.Assigned));

                var filtered_columns = columns.Where(property => set_columns?.Any(c => c.Key.Equals(property.ColumnName)) ?? false)?.ToList();
                if (filtered_columns == null || !filtered_columns.Any()) throw new Exception("No columns were mapped.");
                classMap.SelectedProperties = filtered_columns;
                foreach (var property in filtered_columns)
                {
                    dynamicParameters.Add(property.ColumnName, set_columns[property.ColumnName]);
                }
            }

            IPredicate predicate = GetExpressionPredicate<T>(classMap, expression);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Update(classMap, predicate, parameters, ignoreAllKeyProperties);

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        public async Task<bool> UpdateAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout, bool ignoreAllKeyProperties = false) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();

            DynamicParameters dynamicParameters = new DynamicParameters();

            if (properties == null) throw new ArgumentNullException(nameof(properties));

            var set_columns = ReflectionHelper.GetObjectValues(properties);
            if (set_columns != null && set_columns.Any())
            {
                var columns = ignoreAllKeyProperties
                    ? classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                    : classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity || p.KeyType == KeyType.Assigned));

                var filtered_columns = columns.Where(property => set_columns?.Any(c => c.Key.Equals(property.ColumnName)) ?? false)?.ToList();
                if (filtered_columns == null || !filtered_columns.Any()) throw new Exception("No columns were mapped.");
                classMap.SelectedProperties = filtered_columns;
                foreach (var property in filtered_columns)
                {
                    dynamicParameters.Add(property.ColumnName, set_columns[property.ColumnName]);
                }
            }

            IPredicate predicate = GetExpressionPredicate<T>(classMap, expression);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Update(classMap, predicate, parameters, ignoreAllKeyProperties);

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.ExecuteAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        #endregion

        #region Delete DapperImplementor

        public bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            return Delete<T>(connection, classMap, predicate, transaction, commandTimeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetKeyPredicate<T>(classMap, entity);
            return await DeleteAsync<T>(connection, classMap, predicate, transaction, commandTimeout);
        }

        public bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return Delete<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await DeleteAsync<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public bool Delete<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return Delete<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public async Task<bool> DeleteAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return await DeleteAsync<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        protected bool Delete<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Delete(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        protected async Task<bool> DeleteAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Delete(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.ExecuteAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }
        #endregion

        #region GetList DapperImplementor

        public IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetList<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetListAsync<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout);
        }

        public IEnumerable<T> GetList<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetList<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetListAsync<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout);
        }

        public IEnumerable<T> GetList<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return GetList<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return await GetListAsync<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout);
        }

        protected IEnumerable<T> GetList<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected async Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.QueryAsync<T>(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
        }
        #endregion

        #region GetPage DapperImplementor

        public IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetPage<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetPageAsync<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        public IEnumerable<T> GetPage<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetPage<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetPageAsync<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        public IEnumerable<T> GetPage<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return GetPage<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return await GetPageAsync<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction, commandTimeout);
        }

        protected IEnumerable<T> GetPage<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected async Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.QueryAsync<T>(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
        }

        #endregion

        #region GetSet DapperImplementor

        public IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetSet<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetSetAsync<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout);
        }

        public IEnumerable<T> GetSet<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return GetSet<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await GetSetAsync<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout);
        }

        public IEnumerable<T> GetSet<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return GetSet<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        public async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, Expression<Func<T, object>> properties, Expression<Func<T, bool>> expression, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            FillSelectedProperties(classMap, properties);

            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return await GetSetAsync<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction, commandTimeout);
        }

        protected IEnumerable<T> GetSet<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected async Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return await connection.QueryAsync<T>(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
        }

        #endregion

        #region Count DapperImplementor

        public int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return Count<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public async Task<int> CountAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetPredicate(classMap, predicate);
            return await CountAsync<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public int Count<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return Count<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public async Task<int> CountAsync<T>(IDbConnection connection, Expression<Func<T, bool>> expression, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            IClassMapper classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate wherePredicate = GetExpressionPredicate(classMap, expression);
            return await CountAsync<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        protected int Count<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Count(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return (int)connection.QueryFirstOrDefault(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text).Total;
        }

        protected async Task<int> CountAsync<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string sql = SqlGenerator.Count(classMap, predicate, parameters);
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return (int)(await connection.QueryFirstOrDefaultAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text)).Total;
        }

        #endregion

        #region GetMultiple DapperImplementor

        public IMultipleResultReader GetMultiple(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            if (SqlGenerator.SupportsMultipleStatements())
            {
                return GetMultipleByBatch(connection, predicate, transaction, commandTimeout);
            }

            return GetMultipleBySequence(connection, predicate, transaction, commandTimeout);
        }

        public async Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            if (SqlGenerator.SupportsMultipleStatements())
            {
                return await GetMultipleByBatchAsync(connection, predicate, transaction, commandTimeout);
            }

            return await GetMultipleBySequenceAsync(connection, predicate, transaction, commandTimeout);
        }

        protected GridReaderResultReader GetMultipleByBatch(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            StringBuilder sql = new StringBuilder();
            foreach (var item in predicate.Items)
            {
                IClassMapper classMap = SqlGenerator.Configuration.GetMap(item.Type);
                IPredicate itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                sql.AppendLine(SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters) + SqlGenerator.Configuration.Dialect.BatchSeperator);
            }

            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            SqlMapper.GridReader grid = connection.QueryMultiple(sql.ToString(), dynamicParameters, transaction, commandTimeout, CommandType.Text);
            return new GridReaderResultReader(grid);
        }

        protected async Task<GridReaderResultReader> GetMultipleByBatchAsync(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            StringBuilder sql = new StringBuilder();
            foreach (var item in predicate.Items)
            {
                IClassMapper classMap = SqlGenerator.Configuration.GetMap(item.Type);
                IPredicate itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                sql.AppendLine(SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters) + SqlGenerator.Configuration.Dialect.BatchSeperator);
            }

            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            SqlMapper.GridReader grid = await connection.QueryMultipleAsync(sql.ToString(), dynamicParameters, transaction, commandTimeout, CommandType.Text);
            return new GridReaderResultReader(grid);
        }

        protected SequenceReaderResultReader GetMultipleBySequence(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            IList<SqlMapper.GridReader> items = new List<SqlMapper.GridReader>();
            foreach (var item in predicate.Items)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                IClassMapper classMap = SqlGenerator.Configuration.GetMap(item.Type);
                IPredicate itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                string sql = SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters);
                DynamicParameters dynamicParameters = new DynamicParameters();
                foreach (var parameter in parameters)
                {
                    dynamicParameters.Add(parameter.Key, parameter.Value);
                }

                SqlMapper.GridReader queryResult = connection.QueryMultiple(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
                items.Add(queryResult);
            }

            return new SequenceReaderResultReader(items);
        }

        protected async Task<SequenceReaderResultReader> GetMultipleBySequenceAsync(IDbConnection connection, GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            IList<SqlMapper.GridReader> items = new List<SqlMapper.GridReader>();
            foreach (var item in predicate.Items)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                IClassMapper classMap = SqlGenerator.Configuration.GetMap(item.Type);
                IPredicate itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                string sql = SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters);
                DynamicParameters dynamicParameters = new DynamicParameters();
                foreach (var parameter in parameters)
                {
                    dynamicParameters.Add(parameter.Key, parameter.Value);
                }

                SqlMapper.GridReader queryResult = await connection.QueryMultipleAsync(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text);
                items.Add(queryResult);
            }

            return new SequenceReaderResultReader(items);
        }

        #endregion


        #region Predicate Convert

        protected IPredicate GetPredicate(IClassMapper classMap, object predicate)
        {
            IPredicate wherePredicate = predicate as IPredicate;
            if (wherePredicate == null && predicate != null)
            {
                wherePredicate = GetEntityPredicate(classMap, predicate);
            }

            return wherePredicate;
        }

        protected IPredicate GetIdPredicate(IClassMapper classMap, object id)
        {
            bool isSimpleType = ReflectionHelper.IsSimpleType(id.GetType());
            var keys = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            IDictionary<string, object> paramValues = null;
            IList<IPredicate> predicates = new List<IPredicate>();
            if (!isSimpleType)
            {
                paramValues = ReflectionHelper.GetObjectValues(id);
            }

            foreach (var key in keys)
            {
                object value = id;
                if (!isSimpleType)
                {
                    value = paramValues[key.Name];
                }

                Type predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);

                IFieldPredicate fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = key.Name;
                fieldPredicate.Value = value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                       {
                           Operator = GroupOperator.And,
                           Predicates = predicates
                       };
        }

        protected IPredicate GetKeyPredicate<T>(IClassMapper classMap, T entity) where T : class
        {
            var whereFields = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            if (!whereFields.Any())
            {
                throw new ArgumentException("At least one Key column must be defined.");
            }

            IList<IPredicate> predicates = (from field in whereFields
                                            select new FieldPredicate<T>
                                            {
                                                Not = false,
                                                Operator = Operator.Eq,
                                                PropertyName = field.Name,
                                                Value = field.PropertyInfo.GetValue(entity, null)
                                            }).Cast<IPredicate>().ToList();

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                       {
                           Operator = GroupOperator.And,
                           Predicates = predicates
                       };
        }

        protected IPredicate GetEntityPredicate(IClassMapper classMap, object entity)
        {
            Type predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);
            IList<IPredicate> predicates = new List<IPredicate>();
            foreach (var kvp in ReflectionHelper.GetObjectValues(entity))
            {
                IFieldPredicate fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = kvp.Key;
                fieldPredicate.Value = kvp.Value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                       ? predicates[0]
                       : new PredicateGroup
                       {
                           Operator = GroupOperator.And,
                           Predicates = predicates
                       };
        }

        # region ExpressionPredicate

        /// <summary>
        /// Convert expression to predicate
        /// </summary>
        protected IPredicate GetExpressionPredicate<T>(IClassMapper classMap, Expression<Func<T, bool>> expression) where T : class
        {
            var whereFields = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            if (!whereFields.Any())
            {
                throw new ArgumentException("At least one Key column must be defined.");
            }

            Expression expr = expression.Body;
            return ConvertToPredicate<T>(expr);
        }

        private static IPredicate ConvertToPredicate<T>(Expression expr) where T : class
        {
            if (expr.NodeType == ExpressionType.OrElse || expr.NodeType == ExpressionType.AndAlso)
            {
                BinaryExpression binExpr = expr as BinaryExpression;
                IList<IPredicate> predList = new List<IPredicate> { ConvertToPredicate<T>(binExpr.Left), ConvertToPredicate<T>(binExpr.Right) };
                GroupOperator op = expr.NodeType == ExpressionType.OrElse ? GroupOperator.Or : GroupOperator.And;
                return Predicates.Group(op, predList.ToArray());
            }
            else if (expr.NodeType == ExpressionType.Equal || expr.NodeType == ExpressionType.NotEqual
                || expr.NodeType == ExpressionType.GreaterThan || expr.NodeType == ExpressionType.GreaterThanOrEqual
                || expr.NodeType == ExpressionType.LessThan || expr.NodeType == ExpressionType.LessThanOrEqual)
            {
                return ConvertBaseExpression<T>(expr as BinaryExpression);
            }
            else if (expr.NodeType == ExpressionType.Call)
            {
                return ConverCallExpression<T>(expr);
            }
            else if (expr.NodeType == ExpressionType.Not)
            {
                return ConverNotInExpression<T>(expr);
            }
            else if (expr.NodeType == ExpressionType.MemberAccess && expr.Type == typeof(bool))
            {
                return ConverBooleanMemberExpression<T>(expr);
            }

            return null;
        }

        private static IPredicate ConvertBaseExpression<T>(Expression expr) where T : class
        {
            BinaryExpression binExpr = expr as BinaryExpression;
            MemberExpression memLeftExpr = binExpr.Left as MemberExpression;
            ConstantExpression conRightExpr = binExpr.Right as ConstantExpression;

            if (memLeftExpr == null)
            {
                if (binExpr.Left.NodeType == ExpressionType.Convert)
                {
                    memLeftExpr = (binExpr.Left as UnaryExpression)?.Operand as MemberExpression;
                }
            }

            if (memLeftExpr != null)
            {
                Operator op = Operator.Eq;
                bool not = false;

                switch (binExpr.NodeType)
                {
                    case ExpressionType.Equal:
                        op = Operator.Eq;
                        break;
                    case ExpressionType.NotEqual:
                        op = Operator.Eq;
                        not = true;
                        break;
                    case ExpressionType.LessThan:
                        op = Operator.Lt;
                        break;
                    case ExpressionType.GreaterThan:
                        op = Operator.Gt;
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        op = Operator.Ge;
                        break;
                    case ExpressionType.LessThanOrEqual:
                        op = Operator.Le;
                        break;
                    default:
                        throw new NotImplementedException($"Not Implemented NodeType : {binExpr.NodeType}");
                }

                return new FieldPredicate<T>
                {
                    PropertyName = memLeftExpr.Member.Name,
                    Operator = op,
                    Not = not,
                    Value = GetExpressionValue(binExpr.Right)
                };
            }

            return null;
        }

        private static IPredicate ConverCallExpression<T>(Expression expr) where T : class
        {
            MethodCallExpression mthdExpr = expr as MethodCallExpression;
            MemberExpression memberExpr = mthdExpr.Object as MemberExpression;

            //判断成员变量是否是数组类型，如果是则进行IN条件构建
            if (memberExpr?.Type.IsGenericType == true || memberExpr?.Type.IsArray == true)
            {
                string propertyName = (mthdExpr.Arguments[0] as MemberExpression).Member.Name;
                object value = Expression.Lambda<Func<object>>(memberExpr).Compile()();

                return new FieldPredicate<T>
                {
                    PropertyName = propertyName,
                    Operator = Operator.Eq,
                    Value = value
                };
            }
            else if (memberExpr == null && mthdExpr?.Method.Name == "Contains")
            {
                string propertyName = (mthdExpr.Arguments[1] as MemberExpression).Member.Name;
                object paramValue = Expression.Lambda<Func<object>>(mthdExpr.Arguments[0]).Compile()();

                return new FieldPredicate<T>
                {
                    PropertyName = propertyName,
                    Operator = Operator.Eq,
                    Value = paramValue
                };
            }
            else
            {
                string propertyName = memberExpr?.Member.Name;
                string methName = mthdExpr?.Method.Name;
                string paramValue = GetExpressionValue(mthdExpr.Arguments[0])?.ToString();

                switch (methName)
                {
                    case "Contains":
                        paramValue = $"%{paramValue}%";
                        break;
                    case "StartsWith":
                        paramValue = $"{paramValue}%";
                        break;
                    case "EndsWith":
                        paramValue = $"%{paramValue}";
                        break;
                    default:
                        throw new NotImplementedException($"Not Implemented Method : {methName}");
                }

                return new FieldPredicate<T>
                {
                    PropertyName = propertyName,
                    Operator = Operator.Like,
                    Value = paramValue
                };
            }
        }

        private static IPredicate ConverBooleanMemberExpression<T>(Expression expr) where T : class
        {
            MemberExpression member = expr as MemberExpression;

            return new FieldPredicate<T>
            {
                PropertyName = member.Member.Name,
                Operator = Operator.Eq,
                Value = true
            };
        }

        private static IPredicate ConverNotInExpression<T>(Expression expr) where T : class
        {
            UnaryExpression tempExpr = expr as UnaryExpression;
            IPredicate predicate = ConverCallExpression<T>(tempExpr.Operand);
            (predicate as FieldPredicate<T>).Not = true;
            return predicate;
        }

        private static object GetExpressionValue(Expression expr)
        {
            ConstantExpression con = expr as ConstantExpression;
            if (con != null)
                return con.Value;

            MemberExpression member = expr as MemberExpression;
            if (member != null)
            {
                if (member.Type == typeof(int))
                    return Expression.Lambda<Func<int>>(member).Compile()();
                else if (member.Type == typeof(double))
                    return Expression.Lambda<Func<double>>(member).Compile()();
                else if (member.Type == typeof(float))
                    return Expression.Lambda<Func<float>>(member).Compile()();
                else if (member.Type == typeof(decimal))
                    return Expression.Lambda<Func<decimal>>(member).Compile()();
                else if (member.Type == typeof(long))
                    return Expression.Lambda<Func<long>>(member).Compile()();
                else if (member.Type == typeof(string))
                    return Expression.Lambda<Func<string>>(member).Compile()();
                else if (member.Type == typeof(bool))
                    return Expression.Lambda<Func<bool>>(member).Compile()();
                else if (member.Type == typeof(Guid))
                    return Expression.Lambda<Func<Guid>>(member).Compile()();
                else if (member.Type == typeof(DateTime))
                    return Expression.Lambda<Func<DateTime>>(member).Compile()();
                else
                    return Expression.Lambda<Func<object>>(member).Compile()();
            }

            var obj = expr as NewExpression;
            if (obj != null && obj.Type.IsValueType)
            {
                var _object = Expression.Convert(obj, typeof(object));
                return Expression.Lambda<Func<dynamic>>(_object).Compile().DynamicInvoke();
            }
            var conv = expr as MethodCallExpression;
            if (conv != null)
            {
                return Expression.Lambda(conv).Compile().DynamicInvoke();
            }
            var nullableObj = expr as UnaryExpression;
            if (nullableObj != null)
            {
                con = nullableObj.Operand as ConstantExpression;
                if (con != null)
                    return con.Value;
            }

            return null;
        }

        #endregion

        #endregion
    }
}
