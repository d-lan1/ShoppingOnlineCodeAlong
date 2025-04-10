using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineSolutionCodeAlong.web.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductDto>> GetCollection();
        Task RemoveCollection();
    }
}
