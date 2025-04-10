using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }
        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public string ErrorMessage { get; set; }
        protected async override Task OnInitializedAsync()
        {
            //Razor component lifecycle event read more here
            //return base.OnInitializedAsync();
            try
            {
                //Clear products in local storage which may be outdated
                await ClearLocalStorage();
                //Set the products to the local storage collection,
                //so we dont have to make a call to the server each time we query product
                Products = await ManageProductsLocalStorageService.GetCollection();


                //We will use the injected shopping cart service to retrieve items
                //And update the shopping cart UI component when the OnShoppingCartChanged event is raised
                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                var totalQty = shoppingCartItems.Sum(i => i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }

            catch (Exception ex)
            {
                //Log exception
                ErrorMessage = ex.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return Products.GroupBy(p => p.CategoryId).OrderBy(g => g.Key);
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductsDto)
        {
            return groupedProductsDto.FirstOrDefault().CategoryName;
        }

        private async Task ClearLocalStorage()
        {
            await ManageProductsLocalStorageService.RemoveCollection();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }  
    }
}
