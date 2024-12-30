using ShopOnlineCodeAlong.Api.Entities;
using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineCodeAlong.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddTiem(CartItemToAddDto cartItemToAdd);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);

    }
}
