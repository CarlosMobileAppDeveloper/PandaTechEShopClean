using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Order;

namespace PandaTechEShop.Services.Order
{
    public interface IOrderService
    {
        Task<OrderResponse> PlaceOrderAsync(OrderInfo order);
        Task<List<OrderByUser>> GetOrdersByUserAsync(int userId);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
    }
}
