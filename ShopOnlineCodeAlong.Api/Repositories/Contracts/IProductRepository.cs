using ShopOnlineCodeAlong.Api.Entities;

namespace ShopOnlineCodeAlong.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        //All the methods in this class will run async and so they return task objects
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);

    }
}
