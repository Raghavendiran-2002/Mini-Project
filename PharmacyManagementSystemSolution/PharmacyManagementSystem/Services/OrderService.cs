using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;

namespace PharmacyManagementSystem.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRepository<int, Order> _orderRepo;
        private readonly IProductRepository<int, Product> _productRepo;

        public OrderService(IRepository<int ,Order> orderRepo, IProductRepository<int , Product> productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
           var order = await _orderRepo.Get(orderId);
           return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
        {
            var order = (await _orderRepo.Get()).Where(o=>o.UserID == userId).ToList();
            return order;
        }

        public async Task<Order> PlaceOrder(int userId, OrderCreationDto orderDto)
        {
            Order order = MapOrderCreationDtoToOrder(orderDto, userId);
            decimal totalAmount = 0;

            foreach (var item in orderDto.OrderItems)
            {
                var product = await _productRepo.Get(item.ProductID);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductID} not found.");
                }

                if (product.Stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product {product.ProductName}.");
                }

                product.Stock -= item.Quantity;

                var orderItem = new OrderItem
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Order = order
                };

                order.OrderItems.Add(orderItem);
                totalAmount += orderItem.Quantity * orderItem.UnitPrice;
            }

            order.TotalAmount = totalAmount;

            await _orderRepo.Add(order);

            return order;
        }

        private Order MapOrderCreationDtoToOrder(OrderCreationDto orderDto, int userId)
        {
            Order order = new Order();
            order.UserID = userId;
            order.ShippingAddress = orderDto.ShippingAddress;
            order.Status = "pending";
            order.OrderItems = new List<OrderItem>();
            return order;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, string status)
        {
           var order = await _orderRepo.Get(orderId);
            order.Status = status;
            order = await _orderRepo.Update(order);
            return order;
        }
    }
}
