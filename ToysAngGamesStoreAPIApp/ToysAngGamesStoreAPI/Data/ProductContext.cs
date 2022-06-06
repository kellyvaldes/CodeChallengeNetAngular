using Microsoft.EntityFrameworkCore;
using ToysAngGamesStoreAPI.Models;

namespace ToysAngGamesStoreAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext() { }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;

    }
}
