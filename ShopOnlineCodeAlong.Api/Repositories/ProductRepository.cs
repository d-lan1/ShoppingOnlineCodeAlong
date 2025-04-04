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
            var product = await shopOnlineDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await shopOnlineDbContext.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var produts = await shopOnlineDbContext.Products
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            return produts;
        }
    }
}
