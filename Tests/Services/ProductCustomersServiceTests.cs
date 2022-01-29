using System.Collections.Generic;
using DevOpsInterview.Models;
using DevOpsInterview.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Services
{
    public class ProductCustomersServiceTests
    {
        [Fact]
        public void ProductCustomersComputationSuccessful()
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

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 0,
                    Cost = new decimal(0.0),
                    Name = null
                },
                new Product()
                {
                    Id = 1,
                    Cost = new decimal(0.0),
                    Name = null
                },
                new Product()
                {
                    Id = 2,
                    Cost = new decimal(0.0),
                    Name = null
                },
                new Product()
                {
                    Id = 3,
                    Cost = new decimal(0.0),
                    Name = null
                },
            };
            var productCustomersService = new ProductCustomersService();
            var productCustomers = productCustomersService.Compute(orders, products);

            productCustomers[0].Id.Should().Be(0);
            productCustomers[0].CustomerIds.Should().Contain(new List<int>()
            {
                1
            });

            productCustomers[1].Id.Should().Be(1);
            productCustomers[1].CustomerIds.Should().Contain(new List<int>
            {
                1
            });

            productCustomers[2].Id.Should().Be(2);
            productCustomers[2].CustomerIds.Should().Contain(new List<int>
            {
                1,
                2
            });

            productCustomers[3].Id.Should().Be(3);
            productCustomers[3].CustomerIds.Should().BeEmpty();
        }
    }


}