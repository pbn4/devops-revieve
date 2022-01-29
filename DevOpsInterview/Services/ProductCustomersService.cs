using System;
using System.Collections.Generic;
using System.Linq;
using DevOpsInterview.Models;

namespace DevOpsInterview.Services
{
    public class ProductCustomersService
    {
        public IList<ProductCustomer> Compute(IList<Order> orders, IList<Product> products)
        {
            var productCustomersSet = new Dictionary<int, HashSet<int>>();
            foreach (var product in products)
            {
                productCustomersSet[product.Id] = new HashSet<int>();
            }

            foreach (var order in orders)
            {
                foreach (var productId in order.Products)
                {
                    var customersSet = productCustomersSet.GetValueOrDefault(productId);
                    if (customersSet == null)
                    {
                        throw new Exception($"product id {productId} does not exist in products list");
                    }
                    customersSet.Add(order.Customer);
                }
            }

            var productCustomerList = productCustomersSet
                .Select(pair => new ProductCustomer
                {
                    Id = pair.Key,
                    CustomerIds = pair.Value.ToList()
                })
                .ToList();

            productCustomerList.Sort(
                (customer, productCustomer) => customer.Id > productCustomer.Id ? 1 : -1);
            
            return productCustomerList;
        }
    }
}