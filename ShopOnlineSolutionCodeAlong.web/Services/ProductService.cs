using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineSolutionCodeAlong.web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnlineSolutionCodeAlong.web.Services
{
    public class ProductService:IProductService
    {
        private readonly HttpClient httpClient;

        //Constructor dependency injection
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var response = await httpClient.GetAsync("api/product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //TODO log Exception
                throw;
            }
        }

        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }

            catch (Exception)
            {
                //TODO log Exception
                throw;
            }
        }
    }
}
