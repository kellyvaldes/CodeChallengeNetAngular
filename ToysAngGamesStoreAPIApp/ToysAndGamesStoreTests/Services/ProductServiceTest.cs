using Moq;
using ToysAngGamesStoreAPI.Data;
using ToysAngGamesStoreAPI.Models;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ToysAndGamesStoreTests
{  

    public class ProductServiceTest
    {
        ProductContext? productContext;
        Mock<ProductContext> mockContext = new();
        ProductService? productService;
        readonly List<Product> products = ProductStore.SearchListProduct();

        //Get Products
        [Theory]
        [InlineData(5)]
        public void GetAllProducts_returns_allExistentProduct(int expected)
        {
            // Arrange
            SetupMocks();

            // Act
            var products = productService.GetAll();

            // Assert
            Assert.Equal(expected, products.Count());
            Assert.IsAssignableFrom<IEnumerable<Product>>(products);
        }

        //Get Product with no null results
        [Theory]
        [MemberData(nameof(ProductStore.Data), MemberType = typeof(ProductStore))]
        public void GetProductById_Returns_NotNullResult(int id, int expected)
        {
            // Arrange            
            SetupMocks();

            mockContext
                .Setup(s => s.Products.Find(id))
                .Returns(products.Single(s => s.Id == id));           

            // Act
            var productResult = productService.Get(id);

            // Assert
            Assert.NotNull(productResult);
            Assert.IsType<Product>(productResult);
            Assert.Equal(expected, productResult?.Id);
        }

        //Get Product with null results
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetProductById_Returns_NullResult(int id)
        {
            // Arrange         
            SetupMocks();           
            mockContext
                .Setup(s => s.Products.Find(id))
                .Returns(It.IsAny<Product>());

            // Act
            var product = productService.Get(id);

            // Assert
            Assert.Null(product);            
        }

        //Add        
        [Theory]
        [InlineData(1, "TEST")]
        public void AddProduct_Successfully(int id, string name)
        {
            // Arrange
            SetupMocks();
            int initCountProducts = products.Count;

            mockContext
                .Setup(m => m.Products.Add(It.IsAny<Product>()))
                .Callback<Product>((entity) => products.Add(entity));

            var product = new ProductDTO
            {
                Id = id,
                Name = name
            };
            
            mockContext
                .Setup(s => s.Products.Find(It.IsAny<int>()))
                .Returns(new Product
                {
                    Id = id,
                    Name = name
                });

            // Act
            productService.Add(product);
            var getProduct = productService.Get(id);

            // Assert

            Assert.Equal(name, getProduct.Name);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
            //Validar
            //Assert.Equal(initCountProducts + 1, mockContext.Object.Products.Count());

        }

        //Update
        [Theory]
        [InlineData(1, "TEST")]
        public void UpdateProduct_Successfully(int id, string name)
        {
            // Arrange
            SetupMocks();

            mockContext
                .Setup(s => s.Products.Find(It.IsAny<int>()))
                .Returns(new Product
                {
                    Id = id,
                    Name = name
                });          

            // Act
            var product = productService.Get(id);
            product.Name = "TEST UPDATE";
            productService.Update(id, product);
            var productSaved = productService.Get(id);

            // Assert
            Assert.NotNull(product);
            Assert.NotNull(productSaved);
            Assert.Equal(product.Name, productSaved.Name);
            Assert.Equal("TEST UPDATE", productSaved.Name);
            //Verify

            mockContext.Verify(s => s.Products.Find(id), Times.Exactly(2));
            mockContext.Verify(s => s.Products.Update(It.Is<Product>(p => p.Id == id)), Times.Once);

        }

        //Remove
        [Theory]
        [InlineData(1)]
        public void DeleteProduct_Successfully(int id)
        {
            // Arrange            
            SetupMocks();
            
            var remainingProducts = products.Count - 1;
            mockContext
                .Setup(m => m.Products.Remove(It.IsAny<Product>()))
                .Callback<Product>((entity) => products.Remove(entity));

            mockContext
                .Setup(s => s.Products.Find(id))
                .Returns(products.Single(s => s.Id == id));

            // Act
            productService.Delete(id);

            // Assert            
            Assert.Equal(products.Count, remainingProducts);

            mockContext.Verify(s => s.Products.Find(id), Times.Once);
            mockContext.Verify(s => s.Products.Remove(It.Is<Product>(p => p.Id == id)), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        //Setup Mocks
        internal void SetupMocks()
        {
            mockContext = new Mock<ProductContext>();           
            mockContext.
                Setup(p => p.Products).
                Returns(DbContextMock.GetQueryableMockDbSet<Product>(products));
            productContext = mockContext.Object;
            productService = new ProductService(productContext);
        }
        
    }

}