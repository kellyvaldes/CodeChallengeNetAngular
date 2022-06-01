using Microsoft.EntityFrameworkCore;
using ToysAngGamesStoreAPI.Models;

namespace ToysAngGamesStoreAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) {
        }

        public DbSet<Product> Products { get; set; }

    }
}
