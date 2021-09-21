using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Models.Complaint;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Models.ShoppingCart;
using Refit;

namespace PandaTechEShop.Services.FurnitureAPI
{
    public interface IFurnitureRefitApi
    {
        [Post("/api/accounts/register")]
        Task RegisterUser([Body] Register newReistration);

        [Post("/api/accounts/login")]
        Task<TokenInfo> Login([Body] Login login);

        [Get("/api/categories")]
        Task<List<CategoryInfo>> GetCategories();

        [Get("/api/products/{productId}")]
        Task<ProductInfo> GetProductById(int productId);

        [Get("/api/products/productsbycategory/{categoryId}")]
        Task<List<ProductInfo>> GetProductsByCategory(int categoryId);

        [Get("/api/products/trendingproducts")]
        Task<List<TrendingProduct>> GetTrendingProducts();

        [Post("/api/ShoppingCartItems")]
        Task AddItemsInCart([Body] AddToCart addToCart);

        [Get("/api/ShoppingCartItems/SubTotal/{userId}")]
        Task<CartSubTotal> GetShoppingCartSubTotal(int userId);

        [Get("/api/ShoppingCartItems/{userId}")]
        Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId);

        [Get("/api/ShoppingCartItems/TotalItems/{userId}")]
        Task<TotalCartItem> GetTotalCartItems(int userId);

        [Delete("/api/ShoppingCartItems/{userId}")]
        Task ClearShoppingCart(int userId);

        [Post("/api/Orders")]
        Task PlaceOrder([Body] OrderInfo order);

        [Get("/api/Orders/OrdersByUser/{userId}")]
        Task<List<OrderByUser>> GetOrdersByUser(int userId);

        [Get("/api/Orders/OrderDetails/{orderId}")]
        Task<List<OrderDetail>> GetOrderDetails(int orderId);

        [Post("/api/Complaints")]
        Task RegisterComplaint([Body] ComplaintInfo complaint);
    }
}
