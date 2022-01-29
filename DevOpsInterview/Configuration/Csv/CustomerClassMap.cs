using CsvHelper.Configuration;
using DevOpsInterview.Models;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class CustomerClassMap: ClassMap<Customer>
    {
        public CustomerClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.FirstName).Index(1).Name("firstname");
            Map(m => m.LastName).Index(2).Name("lastname");
        }
    }
}