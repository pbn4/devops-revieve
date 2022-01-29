using CsvHelper.Configuration;
using DevOpsInterview.Models;
using DevOpsInterview.Utils.CustomCsvConverters;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class OrderClassMap : ClassMap<Order>
    {
        public OrderClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Customer).Index(1).Name("customer");
            Map(m => m.Products).Index(2).Name("products").TypeConverter<IntListConverter>();
        }
    }
}