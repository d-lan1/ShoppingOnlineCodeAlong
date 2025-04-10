using ShopOnlineCodeAlong.Api.Entities;
using ShopOnlineCodeAlong.Modells.Dtos;
using ShopOnlineCodeAlong.Models.Dtos;
using System.Net.NetworkInformation;

namespace ShopOnlineCodeAlong.Api.Extensions
{
    // Data Transfer Object (DTO) Pattern implementation.
    // DTOs act as simple, immutable data containers for transferring information across layers or system boundaries.
    // Using DTOs enhances security by limiting unnecessary exposure of potentially sensitive data,
    // as only cherrypicked fields are exposed to consumers.
    // Additionally, they allow underlying domain/data classes to evolve freely (e.g., adding business logic or fields)
    // without affecting external interfaces or APIs.
    // Furthermore, DTOs may offer performance benefits by reducing object graph complexity,
    // minimizing memory overhead (e.g., fewer references for garbage collection),
    // and enabling more efficient, targeted database queries (e.g., fetching only required columns).
    // Obbviously the negitive here is the enhanced code duplication and upfront cost (time) of creating and maintaining DTOs.


    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.ProductCategory.Id,
                        CategoryName = product.ProductCategory.Name,
                    }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.ProductCategory.Id,
                CategoryName = product.ProductCategory.Name,
            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
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


        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return productCategories.Select(productCategory => new ProductCategoryDto
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                IconCSS = productCategory.IconCSS
            }).ToList();
        }
    }
}
