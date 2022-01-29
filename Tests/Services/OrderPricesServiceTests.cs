using System.Collections.Generic;
using DevOpsInterview.Models;
using DevOpsInterview.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Services
{
    public class OrderPricesServiceTests
    {
        [Fact]
        public void OrderPriceComputationSuccessful()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 0,
                    Customer = 0,
                    Products = new List<int>()
                    {
                    }
                },
                new Order
                {
                    Id = 1,
                    Customer = 1,
                    Products = new List<int>()
                    {
                        0,
                        1,
                        2
                    }
                },
                new Order
                {
                    Id = 2,
                    Customer = 2,
                    Products = new List<int>()
                    {
                        2
                    }
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Id = 0,
                    Name = "P0",
                    Cost = new decimal(1.0),
                },
                new Product
                {
                    Id = 1,
                    Name = "P1",
                    Cost = new decimal(2.0),
                },
                new Product
                {
                    Id = 2,
                    Name = "P2",
                    Cost = new decimal(3.0),
                },
            };
            var orderPriceService = new OrderPricesService();
            var orderPrices = orderPriceService.Compute(orders, products);

            orderPrices[0].Id.Should().Be(0);
            orderPrices[0].Euros.Should().Be(0);

            orderPrices[1].Id.Should().Be(1);
            orderPrices[1].Euros.Should().Be(new decimal(6.0));

            orderPrices[2].Id.Should().Be(2);
            orderPrices[2].Euros.Should().Be(new decimal(3.0));
        }
    }
}