using Rainbow.DapperExtensions.Mapper;

namespace Rainbow.DapperExtensions.Tests.Entities
{
    public class Employees : Entity<int>
    {
        //public int id { get; set; }
        public string company { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string email_address { get; set; }
        public string job_title { get; set; }
        public string business_phone { get; set; }
        public string home_phone { get; set; }
        public string mobile_phone { get; set; }
        public string fax_number { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state_province { get; set; }
        public string zip_postal_code { get; set; }
        public string country_region { get; set; }
        public string web_page { get; set; }
        public string notes { get; set; }
        public byte[] attachments { get; set; }

    }

    public class EmployeesMapper : ClassMapper<Employees>
    {
        public EmployeesMapper()
        {
            Table("employees");
            Map(p => p.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
