using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToysAngGamesStoreAPI.Models;

namespace ToysAngGamesStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productRepository;
        public ProductsController(IProductService productRepository) {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Products/id
        [HttpGet("{id}")]
        public IActionResult GetProductsById(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult PostProduct(ProductDTO product)
        {
            _productRepository.Add(product);
            return Ok(new { message = "Product created" });
        }

        // PUT: api/Products/id
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            var productExists = _productRepository.Get(id);
            if (productExists == null)
            {
                return NotFound();
            }
            _productRepository.Update(id, product);
            return Ok(new { message = "Product updated" });
        }

        // DELETE: api/Products/id
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productExists = _productRepository.Get(id);
            if (productExists == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id, productExists);
            return Ok(new { message = "Product deleted" });
        }


    }
}
