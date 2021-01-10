 using Dapper;
using MySql.Data.MySqlClient;
using Rainbow.DapperExtensions.Sql;
using Rainbow.DapperExtensions.Tests.Entities;
using System.Data;
using System.Linq;
using Xunit;

namespace Rainbow.DapperExtensions.Tests
{
    public class DapperExtensionsTest
    {
        private const string con_str = "server=192.168.88.131;port=3306;database=northwind;user=root;password=root;SslMode=None;";

        [Fact]
        public void TestDapper()
        {
            DapperExtensions.SqlDialect = new MySqlDialect();

            using (IDbConnection conn = new MySqlConnection(con_str))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                const string sql = "select sql_calc_found_rows * from employees limit 5,3;  select found_rows();";
                object args = null;

                //            
                using (var multi = conn.QueryMultiple(sql, args))
                {
                    var employees = multi.Read<Employees>().ToList();
                    var total_count = multi.ReadSingleOrDefault<long>();


                    //dynamic someOtherRow = multi.Read().Single();
                    //int qty = someOtherRow.Quantity, price = someOtherRow.Price;
                }

                //conn.Delete<User>(u => u.Id == 1);
                //User user = new User { Id = 1, Name = "123", Psw = "123", RoleId = "123" };
                //conn.Insert<User>(user);
                //var res = conn.GetList<User>(u => u.Name == "123");
                //conn.Delete<User>(user);


                conn.Close();

                //Assert.NotNull(res);
            }

        }

    }
}
