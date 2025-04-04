using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnlineCodeAlong.Models.Dtos
{
    //This particular DTO class initially may look redundant,
    //but it's a good practice to have a separate DTO class for each entity.
    //for more info see comment at DtoConversions.cs
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconCSS { get; set; }
    }
}
