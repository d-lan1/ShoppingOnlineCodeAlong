using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task DeleteCartItem_Click(int id)
        {
            //Send a request to the server saying delete item
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            //We get the response and remove the item from the clientside items collection and
            //dynamically update the UI component without re-rendering the entire page. This is much faster
            //Than a traditional GET request to fetch and re-render all items in the cart

            RemoveCartItem(id);
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            //This removes the item from the clientside list
            ShoppingCartItems.Remove(cartItemDto);
        }
    }
}
