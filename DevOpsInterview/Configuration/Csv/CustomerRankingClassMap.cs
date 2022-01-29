using CsvHelper.Configuration;
using DevOpsInterview.Models;

namespace DevOpsInterview.Configuration.Csv
{
    public sealed class CustomerRankingClassMap : ClassMap<CustomerRanking>
    {
        public CustomerRankingClassMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.FirstName).Index(1).Name("firstname");
            Map(m => m.LastName).Index(2).Name("lastname");
            Map(m => m.TotalEuros).Index(3).Name("total_euros");
        }
    }
}