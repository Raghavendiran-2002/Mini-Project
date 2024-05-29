using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<Order> PlaceOrder(int userId, OrderCreationDto orderDto);
        public Task<IEnumerable<Order>> GetUserOrders(int userId);
        
        public  Task<Order> GetOrderById(int orderId);

        public Task<Order> UpdateOrderStatus(int orderId, string status);

        public Task<Order> CancelOrder(int orderId);
    }
}
