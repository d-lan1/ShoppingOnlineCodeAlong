using Microsoft.EntityFrameworkCore;
using ShopOnlineCodeAlong.Api.Data;
using ShopOnlineCodeAlong.Api.Entities;
using ShopOnlineCodeAlong.Modells.Dtos;

namespace ShopOnlineCodeAlong.Api.Repositories.Contracts
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }
        public async Task<CartItem> AddTiem(CartItemToAddDto cartItemToAdd)
        {
            if (await CartItemExists(cartItemToAdd.CartId, cartItemToAdd.ProductId) == false)
            {

                //linq query syntax - this is syntactic sugar on the linq method syntax below. Both compile to the same thing in asp.net
                var item = await (from product in this.shopOnlineDbContext.Products
                                  where product.Id == cartItemToAdd.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAdd.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAdd.Qty,
                                  }).SingleOrDefaultAsync();

                //linq method syntax
                //var item = await this.shopOnlineDbContext.Products
                //            .Where(product => product.Id == cartItemToAdd.ProductId)
                //            .Select(product => new CartItem
                //            {
                //                CartId = cartItemToAdd.CartId,
                //                ProductId = product.Id,
                //                Qty = cartItemToAdd.Qty,
                //            })
                //            .SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.shopOnlineDbContext.CartItems.AddAsync(item);
                    await this.shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int cartId)
        {
            //Very similar code to GetItems, i smell duplication
            return await this.shopOnlineDbContext.CartItems.Where(i => i.CartId == cartId).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            //This code is almost equivalent to the below code with the exception of some assumptions: each user has 1 cart, cartId is never null
            //Note that the performance of this code is slower than the authros since it makes two queries instead of 1, however this can easily be fixed.
            //this could be fixed with a join query which is what the original code does or some alternate way
            var cart = await shopOnlineDbContext.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            if (cart == null)
            {
                return Enumerable.Empty<CartItem>();
            }

            return await shopOnlineDbContext.CartItems.Where(i => i.CartId == cart.Id).ToListAsync();

            //This is the code by the tutor in the tutorial, personally i hate it in comparison to my code above as the cognitive load is much greater
            //return await (from cart in this.shopOnlineDbContext.Carts
            //              join cartItem in this .shopOnlineDbContext.CartItems
            //              on cart.Id equals cartItem.Id
            //              where cart.UserId == userId
            //              select new CartItem{
            //                  Id = cartItem.Id,
            //                  ProductId= cartItem.ProductId,
            //                  Qty = cartItem.Qty,
            //                  CartId= cartItem.CartId,
            //              }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
