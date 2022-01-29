using System.Collections.Generic;
using System.Linq;
using DevOpsInterview.Models;
using DevOpsInterview.Utils.ExtensionMethods;

namespace DevOpsInterview.Services
{
    public interface ICustomerRankingService
    {
        public IList<CustomerRanking> Compute(IList<Order> orders, IList<Product> products, IList<Customer> customers);
    }
    
    public class CustomerRankingService : ICustomerRankingService
    {
        public IList<CustomerRanking> Compute(IList<Order> orders, IList<Product> products, IList<Customer> customers)
        {
            // This will keep a list of all the ordered products for every given customer ID key
            // e.g. 0, [1,2,3,4,5] - means customer 0 bought products 1, 2, 3, 4 and 5.
            var customersOrderedProductsDictionary = new Dictionary<int, IList<int>>();
            foreach (var customer in customers)
            {
                customersOrderedProductsDictionary[customer.Id] = new List<int>();
            }

            foreach (var order in orders)
            {
                var orderedProducts = customersOrderedProductsDictionary[order.Customer];
                foreach (var productId in order.Products)
                {
                    orderedProducts.Add(productId);
                }
            }

            var productIdDictionary = products.ToIdDictionary();
            var customersIdDictionary = customers.ToIdDictionary();
            var customersRankingList = customersOrderedProductsDictionary.Select(pair => new CustomerRanking()
            {
                Id = pair.Key,
                FirstName = customersIdDictionary[pair.Key].FirstName,
                LastName = customersIdDictionary[pair.Key].LastName,
                TotalEuros = pair.Value.Aggregate(
                    new decimal(0), 
                    (aggregate, productId) => aggregate + productIdDictionary[productId].Cost)
            }).ToList();
            
            customersRankingList.Sort((ranking, otherRanking) => otherRanking.TotalEuros.CompareTo(ranking.TotalEuros));
            
            return customersRankingList;
        }
    }
}