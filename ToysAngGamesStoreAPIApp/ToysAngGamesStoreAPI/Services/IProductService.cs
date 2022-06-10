namespace ToysAngGamesStoreAPI.Models
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? Get(int id);
        void Add(ProductDTO product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
