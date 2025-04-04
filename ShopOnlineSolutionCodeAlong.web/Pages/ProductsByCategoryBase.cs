using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ProductsByCategoryBase:ComponentBase
    {
        //CategoryId property below will be automatically populated based on the parameter value
        //provided in the URL due to the [parameter] attribute.
        //Blazor's Parameter attribute signifies that this property can be assigned from a URL query string
        //or route parameter when navigating to the component. To ensure this works as expected,
        //the routing for the component needs to define the CategoryId as part of its route template (.razor),
        //such as:
        //@page "/products/category/{CategoryId:int}"
        [Parameter]
        public int CategoryId { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        //This method is called only if the GetProductsByCategory CategoryId is set
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Products = await ProductService.GetItemsByCategory(CategoryId);
                if(Products != null && Products.Count() > 0)
                {
                    CategoryName = Products.FirstOrDefault().CategoryName;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
