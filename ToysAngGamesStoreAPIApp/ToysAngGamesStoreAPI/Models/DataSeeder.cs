using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToysAngGamesStoreAPI.Data;

namespace ToysAngGamesStoreAPI.Models
{
    public class DataSeeder
    {
        private readonly ProductContext _dataContext;
        public DataSeeder(ProductContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Seed() {
            if (_dataContext.Products.Any())
            {
                var products = new List<Product>() {
                    new Product()
                    {
                        Name = "Nombre1",
                        Description = "Descripcion1",
                        AgeRestriction = 50,
                        Company = "Company1",
                        Price = 500
                    },
                    new Product()
                    {
                        Name = "Nombre2",
                        Description = "Descripcion2",
                        AgeRestriction = 20,
                        Company = "Company2",
                        Price = 200
                    },
                    new Product()
                    {
                        Name = "Nombre3",
                        Description = "Descripcion3",
                        AgeRestriction = 30,
                        Company = "Company3",
                        Price = 300
                    }
                };

                _dataContext.Products.AddRange(products);
                _dataContext.SaveChanges();
            }
        }
    }
}
