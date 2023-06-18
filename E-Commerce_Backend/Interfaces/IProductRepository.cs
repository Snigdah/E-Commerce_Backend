using E_Commerce_Backend.Models.ProductModel;

namespace E_Commerce_Backend.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int pageSize, int pageNumber);
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
