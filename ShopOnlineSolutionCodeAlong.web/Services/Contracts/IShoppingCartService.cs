using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineSolutionCodeAlong.web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddItem(CartItemToAddDto itemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);
        // Delegates are pointers to methods and can have any return type.
        // For example, an Action delegate always returns void, 
        // while a Func delegate always has a return type defined.
        // Any method assigned to a delegate must match the defined signature,
        // meaning it must accept the specified arguments and return type.
        // In this case the method assigned to the Action delegate must return void and accept an int parameter
        event Action<int> OnShoppingCartChanged;

        void RaiseEventOnShoppingCartChanged(int totalQty);
    }
}
