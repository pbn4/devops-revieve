using System.Collections.Generic;
using System.Linq;
using DevOpsInterview.Models;
using DevOpsInterview.Utils.ExtensionMethods;

namespace DevOpsInterview.Services
{
    public interface IOrderPricesService
    {
        public IList<OrderPrice> Compute(IList<Order> orders, IList<Product> products);
    }
    
    public class OrderPricesService : IOrderPricesService
    {
        public IList<OrderPrice> Compute(IList<Order> orders, IList<Product> products)
        {
            var productsMap = products.ToIdDictionary();
            return orders.Select(order =>
            {
                return new OrderPrice()
                {
                    Id = order.Id,
                    Euros = order.Products.Sum(productId => productsMap[productId].Cost)
                };
            }).ToList();
        }
    }
}