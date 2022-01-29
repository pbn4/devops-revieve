using System.CommandLine;
using DevOpsInterview.Commands;
using DevOpsInterview.Configuration;

namespace DevOpsInterview
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            var orderPricesCommand = new OrderPricesCommand
            {
                TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault,
            };
            
            var productCustomersCommand = new ProductCustomersCommand()
            {
                TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault,
            };
            var customerRankingCommand = new CustomerRankingCommand()
            {
                TreatUnmatchedTokensAsErrors = SharedConstants.TreatUnhandledTokensAsErrorsDefault,
            };

            var rootCommand = new RootCommand();
            rootCommand.AddCommand(orderPricesCommand);
            rootCommand.AddCommand(productCustomersCommand);
            rootCommand.AddCommand(customerRankingCommand);

            return rootCommand.Invoke(args);
        }
    }
}