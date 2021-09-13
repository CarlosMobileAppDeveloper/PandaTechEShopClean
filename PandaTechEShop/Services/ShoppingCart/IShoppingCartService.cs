using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.ShoppingCart;

namespace PandaTechEShop.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task<bool> AddItemsInCartAsync(AddToCart addToCart);
        Task<CartSubTotal> GetShoppingCartSubTotalAsync(int userId);
        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId);
        Task<TotalCartItem> GetTotalCartItemsAsync(int userId);
        Task<bool> ClearShoppingCartAsync(int userId);
    }
}
