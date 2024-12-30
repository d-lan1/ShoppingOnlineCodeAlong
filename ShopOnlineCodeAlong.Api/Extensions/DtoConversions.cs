using ShopOnlineCodeAlong.Api.Entities;
using ShopOnlineCodeAlong.Modells.Dtos;
using System.Net.NetworkInformation;

namespace ShopOnlineCodeAlong.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
            IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = productCategory.Id,
                        CategoryName = productCategory.Name,
                    }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product,
           ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = productCategory.Id,
                CategoryName = productCategory.Name,
            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.Id equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price= product.Price,
                        CartId = cartItem.CartId,
                        Qty= cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    });  
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return  new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qty = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    };
        }
    }
}
