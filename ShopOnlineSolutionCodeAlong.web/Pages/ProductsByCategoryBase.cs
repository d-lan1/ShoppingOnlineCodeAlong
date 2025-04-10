using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class ProductsByCategoryBase : ComponentBase
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
        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        //This method is called when the GetProductsByCategory CategoryId is set in the url
        //and the component is either rendered initally or re-rendered upon the CategoryId is updated.
        //This is useful for when the user navigates to a different category
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Products = await GetProductsCollectionByCategoryId(CategoryId);
                if (Products != null && Products.Count() > 0)
                {
                    CategoryName = Products.FirstOrDefault().CategoryName;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task<IEnumerable<ProductDto>> GetProductsCollectionByCategoryId(int categoryId)
        {
            var productCollection = await ManageProductsLocalStorageService.GetCollection();

            //we perform this nullcheck here because the
            //collection returned has no items if the backend returns no items
            if (productCollection != null)
            {
                return productCollection.Where(p => p.CategoryId == categoryId);
            }
            else
            {
                return await ProductService.GetItemsByCategory(categoryId);
            }
        }
    }
}
