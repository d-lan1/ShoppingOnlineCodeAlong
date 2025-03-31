using System.Net.Http.Json;
using Newtonsoft.Json;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;

namespace ShopOnlineSolutionCodeAlong.web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        // The event modifier defines a special type of delegate that can only be invoked
        // by the defining class or subscribed to be external classes, 
        // enabling publisher-subscriber communication pattern
        public event Action<int> OnShoppingCartChanged;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItem(CartItemToAddDto itemToAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/ShoppingCart", itemToAddDto);

                if (response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"HTTP Stauts code: {response.StatusCode} \n Mesage: {message}");
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }

                return default(CartItemDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<List<CartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<CartItemDto>().ToList();

                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"HTTP Stauts code: {response.StatusCode} \n Mesage: {message}");
                }
            }
            catch
            {
                throw;
            }
        }

        public void RaiseEventOnShoppingCartChanged(int totalQty)
        {
            //If OnShoppingCartChanged is not null, it means the event has subscribers
            if (OnShoppingCartChanged != null)
            {
                OnShoppingCartChanged.Invoke(totalQty);
            }
        }

        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return null;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}
