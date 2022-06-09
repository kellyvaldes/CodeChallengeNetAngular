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
using System.Collections;
using System.Web.Http.Results;
using AutoMapper;

namespace ToysAndGamesStoreTests
{
    public class ProductServiceTest
    {
        Mock<ProductContext> mockContext = new();
        private ProductService? service;

        private void SetupMocks()
        {          
            // 2. Setup the returnables
            mockContext.Setup(c => c.Products).Returns(GetQueryableMockDbSet(
               SearchData()
                ));         
            
            service = new ProductService(mockContext.Object);
        }

        [Fact]        
        public void GetAllProducts_returns_allExistentProduct()
        {
            // Arrange
            SetupMocks();

            // Act
            var products = service.GetAll();

            // Assert
            Assert.Equal(5, products.Count());
            Assert.IsAssignableFrom<IEnumerable<Product>>(products);
        }

        [Theory]
        [InlineData(1, 1)]
        public void GetById_returns_correctResult(int input, int expected)
        {
            // Arrange
            SetupMocks();

            // Act
            var result = service.Get(input);

            // Assert
            Assert.IsType<Product>(result);
            Assert.Equal(expected, result?.Id);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {  
            // Arrange
            var service = new ProductService(mockContext.Object);
            // Act
            var notFoundResult = service.Get(0);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
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
        
        private static Product[] SearchData()
        {
            return new Product[]
            {
                new Product { Id= 1, Name = "Producto 1" },
                new Product { Id= 2, Name = "Producto 2" },
                new Product { Id= 3, Name = "Producto 3" },
                new Product { Id= 4, Name = "Producto 4" },
                new Product { Id= 5, Name = "Producto 5" }
            };
        }

    }










    /* public class ProductTest
     {
         private IProductService? _service;
         Mock<ProductContext> mockContext = new();

         private void SetupMocks()
         {
             // 1. Create moq object


             // 2. Setup the returnables
             mockContext.Setup(c => c.Products).Returns(GetQueryableMockDbSet(
                 new Product { Id = 1, Name = "Producto 1" },
                 new Product { Id = 2, Name = "Producto 2" },
                 new Product { Id = 3, Name = "Producto 3" },
                 new Product { Id = 4, Name = "Producto 4" },
                 new Product { Id = 5, Name = "Producto 5" }
                 ));

             // 3. Assign to Object when needed
             _service = ProductRepoMoq.Object;
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





         [Fact]
         public void GetReadersWhenCalledReturnListOfReaders()
         {
             //Arrange the resources
             SetupMocks();
             var service = new ProductService(mockContext);
             int id = 1;

             //Act on the functionality
             Product response = service.Get(id);

             //Assert the result against the expected
             Assert.Equal(response?.Id, id);
         }
     }*/

    /*public ProductTest()
        {
            var mockRepo = new Mock<ProductService>();
            IList<Product> products = new List<Product>()
              {
               new Product
               {
                   Id = 001,
                   Name = "Rose",
                   Company = "0112346844"
                },
               new Product
                {
                   Id = 001,
                   Name = "Rose",
                   Company = "0112346844"
                  }
               };
            mockRepo.Setup(repo =>
            repo.GetAll()).Returns(products.ToList());
            mockRepo.Setup(repo => repo.Get(
               It.IsAny<int>())).Returns((int i) =>
               products.SingleOrDefault(x => x.Id == i));
            mockRepo.Setup(repo => repo.Add(It.IsAny<ProductDTO>()))
            .Callback((Product productData) =>
            {
                productData.Id = products.Count() + 1;
                products.Add(productData);
            }).Verifiable();
            mockRepo.Setup(repo => repo.Delete(It.IsAny<int>(), It.IsAny<Product>()))
            .Callback((Product productData) =>
            {
                productData.Id = products.Count() - 1;
                products.Remove(productData);
            }).Verifiable();
            mockRepo.SetupAllProperties();
            //_mapper = WebAPI.Mapper.Mapper.GetMapper();
            _service = mockRepo.Object;
        }
        */


}