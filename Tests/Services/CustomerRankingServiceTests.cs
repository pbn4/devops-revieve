using System.Collections.Generic;
using DevOpsInterview.Models;
using DevOpsInterview.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Services
{
    public class CustomerRankingServiceTests
    {
        [Fact]
        public void CustomerRankingComputationSuccessful()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 0,
                    Name = "",
                    Cost = new decimal(1.0)
                },
                new Product
                {
                    Id = 1,
                    Name = "",
                    Cost = new decimal(1.5)
                },
                new Product
                {
                    Id = 2,
                    Name = "",
                    Cost = new decimal(2.0)
                },
            };

            var customers = new List<Customer>()
            {
                new Customer
                {
                    Id = 0,
                    FirstName = "a",
                    LastName = "a"
                },
                new Customer
                {
                    Id = 1,
                    FirstName = "b",
                    LastName = "b"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "c",
                    LastName = "c"
                }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 0,
                    Customer = 2,
                    Products = new List<int>()
                    {
                        0,
                        1,
                        2
                    }
                },
                new Order
                {
                    Id = 1,
                    Customer = 2,
                    Products = new List<int>()
                    {
                        0,
                        1,
                        2
                    }
                },
                new Order
                {
                    Id = 1,
                    Customer = 1,
                    Products = new List<int>()
                    {
                        0,
                    }
                }
            };
            var customerRanking = new CustomerRankingService().Compute(orders, products, customers);

            customerRanking[0].Id.Should().Be(2);
            customerRanking[0].TotalEuros.Should().Be(new decimal(9.0));

            customerRanking[1].Id.Should().Be(1);
            customerRanking[1].TotalEuros.Should().Be(new decimal(1.0));

            customerRanking[2].Id.Should().Be(0);
            customerRanking[2].TotalEuros.Should().Be(new decimal(0.0));
        }
    }
}