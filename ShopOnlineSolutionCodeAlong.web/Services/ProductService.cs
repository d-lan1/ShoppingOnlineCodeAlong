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
                var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/product");
                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
