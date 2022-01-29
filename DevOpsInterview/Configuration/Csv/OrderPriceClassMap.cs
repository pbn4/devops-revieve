using CsvHelper.Configuration;
using DevOpsInterview.Models;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class OrderPriceClassMap: ClassMap<OrderPrice>
    {
        public OrderPriceClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Euros).Index(1).Name("euros");
        }
    }
}