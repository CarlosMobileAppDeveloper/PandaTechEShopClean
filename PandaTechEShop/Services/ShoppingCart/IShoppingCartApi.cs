using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.ShoppingCart;
using Refit;

namespace PandaTechEShop.Services.ShoppingCart
{
    public interface IShoppingCartApi
    {
        [Post("/api/ShoppingCartItems")]
        Task AddItemsInCart([Body] AddToCart addToCart, [Authorize("Bearer")] string token);

        [Get("/api/ShoppingCartItems/SubTotal/{userId}")]
        Task<CartSubTotal> GetShoppingCartSubTotal(int userId, [Authorize("Bearer")] string token);

        [Get("/api/ShoppingCartItems/{userId}")]
        Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId, [Authorize("Bearer")] string token);

        [Get("/api/ShoppingCartItems/TotalItems/{userId}")]
        Task<TotalCartItem> GetTotalCartItems(int userId, [Authorize("Bearer")] string token);

        [Delete("/api/ShoppingCartItems/{userId}")]
        Task ClearShoppingCart(int userId, [Authorize("Bearer")] string token);
    }
}
