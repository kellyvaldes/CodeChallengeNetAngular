using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAngGamesStoreAPI.Models;

namespace ToysAndGamesStoreTests
{
    public class ProductStore
    {
        public static List<Product> SearchListProduct()
        {
            return new List<Product>
            {
                 new Product { Id= 1, Name = "Producto 1", Company = "Producto 1", AgeRestriction = 10, Description = "Producto 1", Price = 500 },
                 new Product { Id= 2, Name = "Producto 2", Company = "Producto 2", AgeRestriction = 10, Description = "Producto 2", Price = 500 },
                 new Product { Id= 3, Name = "Producto 3", Company = "Producto 2", AgeRestriction = 10, Description = "Producto 3", Price = 500 },
                 new Product { Id= 4, Name = "Producto 4", Company = "Producto 2", AgeRestriction = 10, Description = "Producto 4", Price = 500 },
                 new Product { Id= 5, Name = "Producto 5", Company = "Producto 2", AgeRestriction = 10, Description = "Producto 5", Price = 500 }
            };
        }

        /*public static Product SearchProduct()
        {
            return new Product
            {
                Id= 1, Name = "Producto 1", Company = "Producto 1", AgeRestriction = 10, Description = "Producto 1", Price = 500
            };
        }*/

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, 1 },
            new object[] { 2, 2 },
            new object[] { 3, 3 },
            new object[] { 4, 4 },
        };

    }
}
