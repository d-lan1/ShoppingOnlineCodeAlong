using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;
using System.Runtime.CompilerServices;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        //Injecting the JSRuntime service to be able to call JS functions from C#
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
                
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        public void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(i => i.TotalPrice).ToString("0.00");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(i => i.Qty);
        }

        public async Task DeleteCartItem_Click(int id)
        {
            //Send a request to the server saying delete item
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            //We get the response and remove the item from the clientside items collection and
            //dynamically update the UI component without re-rendering the entire page. This is much faster
            //Than a traditional GET request to fetch and re-render all items in the cart

            RemoveCartItem(id);
            CartChanged();
            await MakeUpdateQtyButtonVisible(id, false);

        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id, true);
        }
        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        public void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if(item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }

        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            //This removes the item from the clientside list
            ShoppingCartItems.Remove(cartItemDto);
        }

        protected async Task UpdateQtyCartITem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };

                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQty(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    CartChanged();
                    await MakeUpdateQtyButtonVisible(id, true);

                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                    }
                }
               
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
}
