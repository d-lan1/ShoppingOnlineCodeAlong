﻿using Microsoft.EntityFrameworkCore;
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

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await shopOnlineDbContext.Products.ToListAsync();
            return products;
        }
    }
}
