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

        
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        
        public Product? Get(int id)
        {
            return GetProduct(id);
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

        public void Delete(int id)
        {
            var objToDelete = _context.Products.Find(id);

            if (objToDelete != null)
            {
                _context.Products.Remove(objToDelete);
                _context.SaveChanges();
            }
        }

        private Product? GetProduct(int id)
        {
            return _context.Products.Find(id);
        }


    }
}
