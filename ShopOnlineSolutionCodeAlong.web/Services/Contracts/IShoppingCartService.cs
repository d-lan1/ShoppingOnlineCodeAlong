using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineSolutionCodeAlong.web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddItem(CartItemToAddDto itemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);
    }
}
