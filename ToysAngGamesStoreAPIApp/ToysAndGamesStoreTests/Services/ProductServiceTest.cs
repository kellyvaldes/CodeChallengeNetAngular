using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using ToysAngGamesStoreAPI.Data;
using ToysAngGamesStoreAPI.Models;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ToysAndGamesStoreTests
{
    public class ProductServiceTest
    {

        public ProductServiceTest()
        {

        }


        [Fact]
        //[MemberData]
        public void GetAllProducts()
        {
            //Arrange
            var mockContext = new Mock<ProductContext>();
            mockContext.Setup(c => c.Products).Returns(GetQueryableMockDbSet(
                new Product { Name = "Producto 1" },
                new Product { Name = "Producto 2" }
               ));

            var service = new ProductService(mockContext.Object);

            // Act
            var products = service.GetAll();

            // Assert
            Assert.Equal(3, products.Count());
            Assert.IsAssignableFrom<IEnumerable<Product>>(products);

        }










        private static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }











    }
    public class ProductServiceTestBackup
    {
        private DbContextOptions<ProductContext> dbContextOptions;

        public ProductServiceTestBackup()
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