using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using ToysAngGamesStoreAPI.Data;
using ToysAngGamesStoreAPI.Models;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Threading.Tasks;
using System.Linq;

namespace ToysAndGamesStoreTests
{
    public class ProductServiceTest
    {
        private DbContextOptions<ProductContext> dbContextOptions;

        public ProductServiceTest()
        {
            var dbName = $"ProductsDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }


        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            var productService = CreateProductService();

            // Act
            var products = productService.GetAll().ToList();

            // Assert
            Assert.Equal(3, products.Count);
        }

        private ProductService CreateProductService()
        {
            ProductContext dbContext = new ProductContext(dbContextOptions);
            GenerateProductsData(dbContext);
            return new ProductService(dbContext);
        }

        private static void GenerateProductsData(ProductContext context)
        {
            int index = 1;

            while (index <= 3)
            {
                var product = new Product()
                {
                    Id = index,
                    Name = $"Product_{index}",
                    Description = $"Description_{index}",
                    Company = $"Company_{index}",
                    AgeRestriction = 100,
                    Price = 100
                };

                index++;
                context.Products.Add(product);
            }

            context.SaveChanges();
        }

       
    }
}