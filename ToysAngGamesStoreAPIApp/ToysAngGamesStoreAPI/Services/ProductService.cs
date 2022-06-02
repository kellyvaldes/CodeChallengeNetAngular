using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ToysAngGamesStoreAPI.Data;

namespace ToysAngGamesStoreAPI.Models
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet()]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        // GET: api/Product/id
        [HttpGet("{id}")]
        public Product? Get(int id)
        {
            return getProduct(id);
        }

        public void Add(ProductDTO product)
        {
            var item = new Product
            {
                Name = product.Name,
                Description = product.Description,                
                AgeRestriction = product.AgeRestriction,
                Company = product.Company,
                Price = product.Price,
            };
            _context.Add(item);
            _context.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id, Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        private Product? getProduct(int id)
        {
            return _context.Products.Find(id);
        }


    }
}
