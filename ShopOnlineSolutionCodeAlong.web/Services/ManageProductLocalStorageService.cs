using Blazored.LocalStorage;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Services
{
    public class ManageProductLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductService productService;
        
        private const string key = "ProductsCollection";

        public ManageProductLocalStorageService(ILocalStorageService localStorageService,
            IProductService productService)
        {
            this.localStorageService = localStorageService;
            this.productService = productService;
        }
        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key)
                   ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productsCollection = await productService.GetItems();
            if (productsCollection != null)
            {
                await localStorageService.SetItemAsync(key, productsCollection);
            }
            return productsCollection;
        }
    }
}
