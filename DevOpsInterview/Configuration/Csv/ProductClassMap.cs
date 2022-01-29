using CsvHelper.Configuration;
using DevOpsInterview.Models;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class ProductClassMap: ClassMap<Product>
    {
        public ProductClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Name).Index(1).Name("name");
            Map(m => m.Cost).Index(2).Name("cost");
        }
    }
}