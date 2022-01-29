using System.CommandLine;
using DevOpsInterview.Configuration;
using DevOpsInterview.Configuration.Csv;
using DevOpsInterview.Models;
using DevOpsInterview.Services;

namespace DevOpsInterview.Commands
{
    public class CustomerRankingCommand : Command
    {
        private readonly ICsvIoService _csvIoService;
        private readonly ICustomerRankingService _customerRankingService;
        
        public CustomerRankingCommand(ICsvIoService csvIoService, ICustomerRankingService customerRankingService) : base("CustomerRanking")
        {
            _csvIoService = csvIoService;
            _customerRankingService = customerRankingService;
            var ordersCsvFilePathOption = new Option<string>(
                "--orders-csv-file-path",
                "Orders CSV file path")
            {
                IsRequired = true
            };

            var productsCsvFilePathOption = new Option<string>(
                "--products-csv-file-path",
                "Products CSV file path")
            {
                IsRequired = true
            };

            var customersCsvFilePathOption = new Option<string>(
                "--customers-csv-file-path",
                "Customers CSV file path")
            {
                IsRequired = true
            };

            var outputFilePathOption = new Option<string>(
                "--output-file-path",
                () => SharedConstants.CustomerRankingOutputCsvFilePathDefault,
                "Output CSV file path");

            AddOption(ordersCsvFilePathOption);
            AddOption(productsCsvFilePathOption);
            AddOption(customersCsvFilePathOption);
            AddOption(outputFilePathOption);

            this.SetHandler<string, string, string, string>(
                Handler,
                ordersCsvFilePathOption,
                productsCsvFilePathOption,
                customersCsvFilePathOption,
                outputFilePathOption);
        }

        private new void Handler(
            string ordersCsvFilePath, 
            string productsCsvFilePath, 
            string customersCsvFilePath, 
            string outputFilePath)
        {
            var orders = _csvIoService.Load<Order, OrderClassMap>(ordersCsvFilePath);
            var products = _csvIoService.Load<Product, ProductClassMap>(productsCsvFilePath);
            var customers = _csvIoService.Load<Customer, CustomerClassMap>(customersCsvFilePath);

            var customerRankings = _customerRankingService.Compute(orders, products, customers);

            _csvIoService.Save<CustomerRanking, CustomerRankingClassMap>(outputFilePath, customerRankings);
        }
    }
}