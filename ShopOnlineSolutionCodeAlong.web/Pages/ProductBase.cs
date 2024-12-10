using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService {get;set;}
        public IEnumerable<ProductDto> Products { get; set; }
        protected async override Task OnInitializedAsync()
        {
            //Razor component lifecycle event read more here
            //return base.OnInitializedAsync();

            //We will use the injected product service to retrieve items
            Products = await ProductService.GetItems();

        }
    }
}
