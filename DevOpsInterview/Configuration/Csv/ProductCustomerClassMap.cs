using CsvHelper.Configuration;
using DevOpsInterview.Models;
using DevOpsInterview.Utils.CustomCsvConverters;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class ProductCustomerClassMap : ClassMap<ProductCustomer>
    {
        public ProductCustomerClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.CustomerIds).Index(1).Name("customer_ids").TypeConverter<IntListConverter>();
        }
    }
}