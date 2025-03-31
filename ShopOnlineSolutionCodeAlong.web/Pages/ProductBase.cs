using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService {get;set;}
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public string ErrorMessage { get; set; }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                //Razor component lifecycle event read more here
                //return base.OnInitializedAsync();

                //We will use the injected product service to retrieve items
                Products = await ProductService.GetItems();

                //We will use the injected shopping cart service to retrieve items
                //And update the shopping cart UI component when the OnShoppingCartChanged event is raised
                var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
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
    }
}
