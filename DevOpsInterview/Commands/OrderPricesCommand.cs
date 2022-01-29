using System.CommandLine;
using DevOpsInterview.Configuration;
using DevOpsInterview.Configuration.Csv;
using DevOpsInterview.Models;
using DevOpsInterview.Services;

namespace DevOpsInterview.Commands
{
    public class OrderPricesCommand : Command
    {
        private readonly ICsvIoService _csvIoService;
        private readonly IOrderPricesService _orderPricesService;

        public OrderPricesCommand(ICsvIoService csvIoService, IOrderPricesService orderPricesService) : base("OrderPrices")
        {
            _csvIoService = csvIoService;
            _orderPricesService = orderPricesService;

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

        private new void Handler(string ordersCsvFilePath, string productsCsvFilePath, string outputFilePath)
        {
            var orders = _csvIoService.Load<Order, OrderClassMap>(ordersCsvFilePath);
            var products = _csvIoService.Load<Product, ProductClassMap>(productsCsvFilePath);

            var orderPrices = _orderPricesService.Compute(orders, products);

            _csvIoService.Save<OrderPrice, OrderPriceClassMap>(outputFilePath, orderPrices);
        }
    }
}