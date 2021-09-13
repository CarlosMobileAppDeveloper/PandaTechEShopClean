using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Order;
using Refit;

namespace PandaTechEShop.Services.Order
{
    public interface IOrderApi
    {
        [Post("/api/Orders")]
        Task PlaceOrder([Body] OrderInfo order, [Authorize("Bearer")] string token);

        [Get("/api/Orders/OrdersByUser/{userId}")]
        Task<List<OrderByUser>> GetOrdersByUser(int userId, [Authorize("Bearer")] string token);

        [Get("/api/Orders/OrderDetails/{orderId}")]
        Task<List<OrderDetail>> GetOrderDetails(int orderId, [Authorize("Bearer")] string token);
    }
}
