using Rainbow.DapperExtensions.Mapper;

namespace Rainbow.DapperExtensions.Tests.Entities
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class RoleMapper : ClassMapper<Role>
    {
        public RoleMapper()
        {
            Table("Role");
            AutoMap();
        }
    }
}
