using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components;
using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineSolutionCodeAlong.web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
