using Autofac;
using Dapper;
using MediatR;
using MySql.Data.MySqlClient;
using Rainbow.DapperExtensions.Repository;
using Rainbow.DapperExtensions.Tests.Attribute;
using Rainbow.DapperExtensions.Tests.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rainbow.DapperExtensions.Tests
{
    public class AppRepositoryTest
    {
        const string connection = "server=192.168.88.131;port=3306;database=northwind;user=root;password=root;SslMode=None;";
        private readonly AppDbContext _context = new AppDbContext(connection);
        private readonly UowDbContext _uowcontext = new UowDbContext(connection, BuildMediator());

        private Employees employ = new Employees
        {
            company = "Tencent",
            first_name = "Pony",
            last_name = "Ma",
            city = "Shenzhen"
        };

        [Fact]
        public void TestGuidAndEnum()
        {
            using (_context)
            {
                //get
                var entry = _context.Get<IntegrationEventLogEntry>(x => x.State == EventStateEnum.Published);

                entry = new IntegrationEventLogEntry
                {
                    EventId = Guid.NewGuid(),
                    State = (EventStateEnum)Enum.Parse(typeof(EventStateEnum), "2"),
                };
                //add
                Guid _id = _context.Add(entry);
            }
        }


        //[Fact]
        [Theory(DisplayName = "Entity Repostity CRUD Sync Test")]
        [Repeat(1000)]
        public void EntityRepostity(int iterationNumber)
        {
            //init sql & parameters
            int page_index = 1, page_size = 10;
            var sql_params = new DynamicParameters(new { skip = (page_index - 1) * page_size, take = page_size });
            sql_params.Add("@city", "Seattle");
            sql_params.Add("@job_title", "Sales Representative");

            StringBuilder sql_script = new StringBuilder();
            sql_script.AppendLine("select sql_calc_found_rows * from employees where 1=1");
            sql_script.AppendLine("and city = @city");
            sql_script.AppendLine("and job_title=@job_title");

            if (page_size > 0)
            {
                sql_script.AppendLine("limit @skip,@take;");
                sql_script.AppendLine("select found_rows();");
            }

            using (_context)
            {
                //multi-query
                var multi = _context.QueryMultiple(sql_script.ToString(), sql_params);
                var employees = multi.Read<Employees>();
                var total_count = multi.ReadFirstOrDefault<int>();

                //paged
                var (data, count) = _context.GetPage<Employees>(sql_script.ToString(), sql_params);

                //add
                _context.Add(new[] { employ } as IEnumerable<Employees>);

                int employee_id = _context.Add(employ);

                //get
                var emp = _context.Get<Employees>(x => x.Id == employee_id);
                emp.state_province = "ZheJiang";

                //update 
                _context.Update<Employees>(e => new { address = "1st Avenue", zip_postal_code = (string)null }, p => p.Id == employee_id);
                _context.Update<Employees>(emp);

                //query part
                IList<ISort> sorts = new List<ISort>() { new Sort("Id", false) };
                var t2 = _context.GetList<Employees>(e => e.last_name.Contains("Ma") && e.Id < Math.Abs(-100),
                    m => new { m.Id, m.first_name, m.last_name, m.city, m.job_title }, sorts);

                //delete
                _context.Delete<Employees>(e => e.Id == employee_id);

            }
        }


        //[Fact]
        [Theory(DisplayName = "Entity Repostity CRUD Async Test")]
        [Repeat(1000)]
        public async Task EntityRepostityAsync(int iterationNumber)
        {
            //init sql & parameters
            int page_index = 1, page_size = 10;
            var sql_params = new DynamicParameters(new { skip = (page_index - 1) * page_size, take = page_size });
            sql_params.Add("@city", "Seattle");
            sql_params.Add("@job_title", "Sales Representative");

            StringBuilder sql_script = new StringBuilder();
            sql_script.AppendLine("select sql_calc_found_rows * from employees where 1=1");
            sql_script.AppendLine("and city = @city");
            sql_script.AppendLine("and job_title=@job_title");

            if (page_size > 0)
            {
                sql_script.AppendLine("limit @skip,@take;");
                sql_script.AppendLine("select found_rows();");
            }

            using (_context)
            {
                //multi-query
                var multi = await _context.QueryMultipleAsync(sql_script.ToString(), sql_params);
                var employees = await multi.ReadAsync<Employees>();
                var total_count = await multi.ReadFirstOrDefaultAsync<int>();

                //paged
                var (data, count) = await _context.GetPageAsync<Employees>(sql_script.ToString(), sql_params);

                //add
                int employee_id = await _context.AddAsync(employ);

                //get
                var emp = await _context.GetAsync<Employees>(x => x.Id == employee_id);
                emp.state_province = "GuangDong";

                //update 
                await _context.UpdateAsync<Employees>(e => new { address = "Tencent Binhai Building, No. 33, Haitian Second Road, Nanshan District, Shenzhen", zip_postal_code = "518054" }, p => p.Id == employee_id);
                await _context.UpdateAsync<Employees>(emp);

                //query part
                IList<ISort> sorts = new List<ISort>() { new Sort("Id", false) };
                var t2 = await _context.GetListAsync<Employees>(e => e.last_name.Contains("Ma") && e.Id < Math.Abs(-100),
                    m => new { m.Id, m.first_name, m.last_name, m.city, m.job_title }, sorts);

                //delete
                await _context.DeleteAsync<Employees>(e => e.Id == employee_id);
            }
        }


        [Fact]
        public void TestUow()
        {
            Employees employ = new Employees
            {
                company = "Alibaba",
                first_name = "Jack",
                last_name = "Ma",
                city = "Hangzhou"
            };


            IEmployeeRepository employeeRepository = new EmployeeRepository(_uowcontext);

            //UoW
            employ.AddDomainEvent(new TestEvent("addEvent"));
            var id = employeeRepository.Add(employ);

            var employee = employeeRepository.GetAsync(id).Result;

            employee.job_title = "CEO";

            employee.AddDomainEvent(new TestEvent("changeEvent"));
            employeeRepository.Update(employee);

            employeeRepository.UnitOfWork.SaveEntitiesAsync();

        }

        public class TestEvent : INotification
        {
            public TestEvent() { }
            public TestEvent(string name) { Name = name; }
            public string Name { get; set; }

        }

        public class TestCommandHandler : INotificationHandler<TestEvent>
        {
            public Task Handle(TestEvent notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"======== Handle : {notification.Name} ========");
                return Task.CompletedTask;
            }
        }

        public class EmployeeRepository : IEmployeeRepository
        {
            private readonly UowDbContext _context;

            public IUnitOfWork UnitOfWork
            {
                get
                {
                    return _context;
                }
            }

            public EmployeeRepository(UowDbContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public int Add(Employees employee)
            {
                return _context.Add(employee);
            }

            public async Task<Employees> GetAsync(int employeeId)
            {
                var order = await Task.Run(() => _context.Get<Employees>(x => x.Id == employeeId));

                return order;
            }

            public void Update(Employees employee)
            {
                _context.Update(employee);
            }
        }

        public interface IEmployeeRepository : IRepository<Employees>
        {
            int Add(Employees employe);

            void Update(Employees employe);

            Task<Employees> GetAsync(int employeId);
        }


        public sealed class AppDbContext : EntityRepository
        {
            private readonly string connection;

            public AppDbContext(string connectionString) : base(connectionString)
            {
                connection = connectionString ?? throw new ArgumentNullException(nameof(connectionString));


            }

            protected override IDbConnection GetConnection() => new MySqlConnection(connection);

            //Extension Method

            public (IEnumerable<T>, int) GetPage<T>(string sql, object param = null, CommandType? commandType = null) where T : class
                => Execute(() =>
                {
                    var rlt = base.QueryMultiple(sql, param, commandType);
                    return (rlt.Read<T>(), rlt.ReadFirstOrDefault<int>());
                }, SqlType.DQL);

            public async Task<(IEnumerable<T>, int)> GetPageAsync<T>(string sql, object param = null, CommandType? commandType = null) where T : class
                => await ExecuteAsync(async () =>
                {
                    var rlt = await base.QueryMultipleAsync(sql, param, commandType);
                    return (await rlt.ReadAsync<T>(), await rlt.ReadFirstOrDefaultAsync<int>());
                }, SqlType.DQL);
        }


        /// <summary>
        /// Unit Of Work Pattern
        /// </summary>
        public sealed class UowDbContext : EntityRepository, IUnitOfWork
        {
            private readonly string _connectionString;
            private static readonly SqlType[] SqlTypes = new SqlType[] { SqlType.Unknown, SqlType.DQL };
            private readonly ConcurrentQueue<INotification> _domianEvents = new ConcurrentQueue<INotification>();
            private readonly IMediator _mediator;

            public new string TransactionId => !HasActiveTransaction ? string.Empty : base.TransactionId.ToString();

            public UowDbContext(string connectionString, IMediator mediator) : base(connectionString)
            {
                _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
                _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            }

            protected override IDbConnection GetConnection() => new MySqlConnection(_connectionString);


            protected override TResult Execute<TResult>(Func<TResult> fun, SqlType sqlType = SqlType.Unknown)
            {
                TResult result = default;

                if (Connection != null && fun != null)
                {
                    try
                    {
                        base.TryOpenConnection();

                        if (!SqlTypes.Contains(sqlType) && this is IUnitOfWork)
                        {
                            //Begin Transaction
                            base.BeginTransaction();

                            //Get DomainEvents
                            FillDomainEvents(fun.Target);
                        }

                        result = fun.Invoke();
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
            protected override void Execute(Action action, SqlType sqlType = SqlType.Unknown)
            {
                if (Connection != null && action != null)
                {
                    try
                    {
                        base.TryOpenConnection();
                        if (!SqlTypes.Contains(sqlType) && this is IUnitOfWork)
                        {
                            base.BeginTransaction();
                        }
                        action.Invoke();
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

            protected override async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> fun, SqlType sqlType = SqlType.Unknown)
            {
                TResult result = default;
                if (Connection != null && fun != null)
                {
                    try
                    {
                        TryOpenConnection();

                        if (!SqlTypes.Contains(sqlType) && this is IUnitOfWork)
                        {
                            //Begin Transaction
                            base.BeginTransaction();

                            //Get DomainEvents
                            FillDomainEvents(fun.Target);
                        }

                        result = await fun.Invoke();
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
            protected override async Task ExecuteAsync(Func<Task> action, SqlType sqlType = SqlType.Unknown)
            {
                if (Connection != null && action != null)
                {
                    try
                    {
                        TryOpenConnection();
                        if (!SqlTypes.Contains(sqlType) && this is IUnitOfWork)
                        {
                            base.BeginTransaction();
                        }
                        await action.Invoke();
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

            private void FillDomainEvents(object target)
            {
                //fill DomainEvents
                if (target != null)
                {
                    foreach (var field in target.GetType().GetFields())
                    {
                        var filedName = field.Name;
                        dynamic filedValue = field.GetValue(target);

                        if (filedValue != null && filedValue is IEntity)
                        {
                            IReadOnlyCollection<INotification> domainEvents = filedValue.DomainEvents;
                            if (filedValue != null && domainEvents?.Count > 0)
                            {
                                domainEvents.ToList().ForEach(e => _domianEvents.Enqueue(e));
                                filedValue.ClearDomainEvents();
                            }
                        }
                    }
                }
            }


            public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
            {
                if (_domianEvents?.Count > 0)
                {
                    foreach (var domainEvent in _domianEvents)
                    {
                        _mediator.Publish(domainEvent);
                    }
                    _domianEvents.Clear();
                }

                base.Commit();
                return Task.FromResult(true);
            }


            public (IEnumerable<T>, int) GetPage<T>(string sql, object param = null, CommandType? commandType = null) where T : class
                => Execute(() =>
                {
                    var rlt = base.QueryMultiple(sql, param, commandType);
                    return (rlt.Read<T>(), rlt.Read<int>()?.First() ?? 0);
                }, SqlType.DQL);
        }


        public interface IUnitOfWork : IDisposable
        {
            Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        }

        public interface IRepository<in T> where T : class
        {
            IUnitOfWork UnitOfWork { get; }
        }

        private static IMediator BuildMediator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();  //注册IMediator自身的组件

            var mediatrOpenTypes = new[] { typeof(IRequestHandler<,>), typeof(IRequestHandler<>), typeof(INotificationHandler<>) };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(mediatrOpenType).AsImplementedInterfaces();
            }

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();

            return mediator;
        }


    }
}
