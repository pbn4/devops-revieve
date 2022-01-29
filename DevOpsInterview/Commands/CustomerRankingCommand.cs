using System.CommandLine;
using DevOpsInterview.Configuration;
using DevOpsInterview.Configuration.Csv;
using DevOpsInterview.Models;
using DevOpsInterview.Services;

namespace DevOpsInterview.Commands
{
    public class CustomerRankingCommand : Command
    {
        public CustomerRankingCommand() : base("CustomerRanking")
        {
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

        private new static void Handler(
            string ordersCsvFilePath, 
            string productsCsvFilePath, 
            string customersCsvFilePath, 
            string outputFilePath)
        {
            var csvIoService = new CsvIoService();

            var orders = csvIoService.Load<Order, OrderClassMap>(ordersCsvFilePath);
            var products = csvIoService.Load<Product, ProductClassMap>(productsCsvFilePath);
            var customers = csvIoService.Load<Customer, CustomerClassMap>(customersCsvFilePath);

            var customerRankings = new CustomerRankingService().Compute(orders, products, customers);

            csvIoService.Save<CustomerRanking, CustomerRankingClassMap>(outputFilePath, customerRankings);
        }
    }
}