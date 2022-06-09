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
using EntityFramework.Testing;

namespace ToysAndGamesStoreTests
{  

    public class ProductServiceTest
    {
        ProductContext? productContext;

        [Theory]
        [InlineData(5)]
        public void GetAllProducts_returns_allExistentProduct(int expected)
        {
            // Arrange
            SetupMocks();
            var service = new ProductService(productContext);

            // Act
            var products = service.GetAll();

            // Assert
            Assert.Equal(expected, products.Count());
            Assert.IsAssignableFrom<IEnumerable<Product>>(products);
        }

        [Theory]
        [MemberData(nameof(ProductStore.Data), MemberType = typeof(ProductStore))]
        public void GetProductById_Returns_NotNullResult(int id, int expected)
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(p=>p.Get(It.IsAny<int>()))
                .Returns(new Product()
                {
                    Id = id
                });

            var productService = mockProductService.Object;

            // Act
            var productResult = productService.Get(id);

            // Assert
            Assert.NotNull(productResult);
            Assert.IsType<Product>(productResult);
            Assert.Equal(expected, productResult?.Id);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetProductById_Returns_NullResult(int id)
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(p => p.Get(It.IsAny<int>()))
                .Returns(It.IsAny<Product>());

            var productService = mockProductService.Object;

            // Act
            var product = productService.Get(id);

            // Assert
            Assert.Null(product);            
        }


        //Add



        //Update
        [Theory]
        [InlineData(1, "TEST")]
        public void UpdateProduct_Returns_Ok(int id, string name)
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(p => p.Get(It.IsAny<int>()))
                .Returns(new Product { 
                        Id = id,
                        Name = name
                    });

            var productService = mockProductService.Object;

            var product = productService.Get(1);
            product.Name = "TEST UPDATE";
            productService.Update(1, product);
            var productSaved = productService.Get(1);

            Assert.NotNull(product);
            Assert.NotNull(productSaved);
            Assert.Equal(product.Name, productSaved.Name);
            Assert.Equal("TEST UPDATE", productSaved.Name);
        }




        //Remove



        internal void SetupMocks()
        {
            var mockContext = new Mock<ProductContext>();
            mockContext.Setup(p => p.Products).Returns(DbContextMock.GetQueryableMockDbSet<Product>(ProductStore.SearchListProduct()));
            productContext = mockContext.Object;
        }

        
    }








    /* public class ProductServiceTest
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

     */
}