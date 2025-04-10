using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineSolutionCodeAlong.web.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<List<CartItemDto>> GetCollection();
        Task SaveCollection(List<CartItemDto> cartItems);
        Task RemoveCollection();
    }
}
