using E_Commerce_Backend.Data;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;    
        }

        //Get Product
        public async Task<IEnumerable<Product>> GetProducts(int pageSize, int pageNumber)
        {
            var products = await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }

        //Get Product By ID
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(i => i.ProductId == id);
            return product;
        }

        //Add Product
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        //Update Product
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        //Delete Product
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }   
    }
}
