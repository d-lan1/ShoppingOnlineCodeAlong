using Blazored.LocalStorage;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Services
{
    public class ManageCartIemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IShoppingCartService shoppingCartService;
        private const string key = "CartItemsCollection";

        public ManageCartIemsLocalStorageService(ILocalStorageService localStorageService,
            IShoppingCartService shoppingCartService)
        {
            this.localStorageService = localStorageService;
            this.shoppingCartService = shoppingCartService;
        }
        public async Task<List<CartItemDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<CartItemDto>>(key)
                   ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CartItemDto> cartItems)
        {
            await this.localStorageService.SetItemAsync(key, cartItems);
        }
        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this.shoppingCartService.GetItems(HardCoded.UserId);
            if (shoppingCartCollection != null)
            {
                await localStorageService.SetItemAsync(key, shoppingCartCollection);
            }
            return shoppingCartCollection; 
        }
    }
}
