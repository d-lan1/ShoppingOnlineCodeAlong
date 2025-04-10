using Microsoft.EntityFrameworkCore;
using ShopOnlineCodeAlong.Api.Data;
using ShopOnlineCodeAlong.Api.Entities;
using ShopOnlineCodeAlong.Api.Repositories.Contracts;

namespace ShopOnlineCodeAlong.Api.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await shopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c =>  c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await shopOnlineDbContext.Products
                .Include(p => p.ProductCategory)
                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await shopOnlineDbContext.Products
                .Include(p => p.ProductCategory)
                .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await shopOnlineDbContext.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            return produts;
        }
    }
}
