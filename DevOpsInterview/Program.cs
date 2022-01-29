using System.CommandLine;
using DevOpsInterview.Commands;
using Microsoft.Extensions.Hosting;
using DevOpsInterview.Configuration;
using DevOpsInterview.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevOpsInterview
{
    internal static class Program
    {
        public static int Main(string[] args)
        {

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<IOrderPricesService, OrderPricesService>();
                    services.AddSingleton<IProductCustomersService, ProductCustomersService>();
                    services.AddSingleton<ICustomerRankingService, CustomerRankingService>();
                    services.AddTransient<ICsvIoService, CsvIoService>();
                    services.AddTransient<OrderPricesCommand>();
                    services.AddTransient<ProductCustomersCommand>();
                    services.AddTransient<CustomerRankingCommand>();
                })
                .Build();

            var orderPricesCommand = host.Services.GetRequiredService<OrderPricesCommand>();
            orderPricesCommand.TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault;

            var productCustomersCommand = host.Services.GetRequiredService<ProductCustomersCommand>();
            productCustomersCommand.TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault;

            var customerRankingCommand = host.Services.GetRequiredService<CustomerRankingCommand>();
            customerRankingCommand.TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault;

            var rootCommand = new RootCommand();
            rootCommand.AddCommand(orderPricesCommand);
            rootCommand.AddCommand(productCustomersCommand);
            rootCommand.AddCommand(customerRankingCommand);

            return rootCommand.Invoke(args);
        }
    }
}