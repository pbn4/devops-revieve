using System.CommandLine;
using DevOpsInterview.Configuration;
using DevOpsInterview.Configuration.Csv;
using DevOpsInterview.Models;
using DevOpsInterview.Services;

namespace DevOpsInterview.Commands
{
    public class ProductCustomersCommand : Command
    {
        private readonly ICsvIoService _csvIoService;
        private readonly IProductCustomersService _productCustomersService;

        public ProductCustomersCommand(ICsvIoService csvIoService, IProductCustomersService productCustomersService) : base("ProductCustomers")
        {
            _csvIoService = csvIoService;
            _productCustomersService = productCustomersService;
            var ordersCsvFilePathOption = new Option<string>(
                "--orders-csv-file-path",
                "Orders CSV file path")
            {
                IsRequired = true
            };

            var productsCsvFilePathOption = new Option<string>(
                "--products-csv-file-path",
                "Orders CSV file path")
            {
                IsRequired = true
            };

            var outputFilePathOption = new Option<string>(
                "--output-file-path",
                () => SharedConstants.ProductCustomersOutputCsvFilePathDefault,
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

            var productCustomers = _productCustomersService.Compute(orders, products);

            _csvIoService.Save<ProductCustomer, ProductCustomerClassMap>(outputFilePath, productCustomers);
        }
    }
}