using System.CommandLine;
using DevOpsInterview.Configuration;
using DevOpsInterview.Configuration.Csv;
using DevOpsInterview.Models;
using DevOpsInterview.Services;

namespace DevOpsInterview.Commands
{
    public class OrderPricesCommand : Command
    {
        public OrderPricesCommand() : base("OrderPrices")
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

            var outputFilePathOption = new Option<string>(
                    "--output-file-path",
                    () => SharedConstants.OrderPricesOutputCsvFilePathDefault,
                    "Output CSV file path");
            
            AddOption(ordersCsvFilePathOption);
            AddOption(productsCsvFilePathOption);
            AddOption(outputFilePathOption);
            
            this.SetHandler<string, string, string>(
                Handler, 
                ordersCsvFilePathOption, 
                productsCsvFilePathOption, 
                outputFilePathOption);
        }

        private new static void Handler(string ordersCsvFilePath, string productsCsvFilePath, string outputFilePath)
        {
            var csvIoService = new CsvIoService();

            var orders = csvIoService.Load<Order, OrderClassMap>(ordersCsvFilePath);
            var products = csvIoService.Load<Product, ProductClassMap>(productsCsvFilePath);

            var orderPrices = new OrderPricesService().Compute(orders, products);

            csvIoService.Save<OrderPrice, OrderPriceClassMap>(outputFilePath, orderPrices);
        }
    }
}