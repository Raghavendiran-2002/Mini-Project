using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class OrderRepository : BaseRepository<int, Order>, IOrderRepository<int, Order>
    {
        public OrderRepository(DBPharmacyContext context) : base(context)
        {

        }

        public override Task<Order> Get(int key)
        {
            return base.Get(key);
        }
        public override async Task<IEnumerable<Order>> Get()
        {

            var order = await _context.Orders.Include(c => c.OrderItems).ToListAsync();
            return order;
        }
        public async Task<Order> CancelOrder(int orderId)
        {
            var order = await _context.Orders
         .Include(o => o.OrderItems)
         .SingleOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            foreach (var orderItem in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(orderItem.ProductID);
                if (product != null)
                {
                    product.Stock += orderItem.Quantity;
                }
            }
            var payment = _context.Payments.Where(p => p.OrderID == orderId).FirstOrDefault();
            if (payment != null)
            {
                payment.Status = "Refunded";
            }
            _context.Payments.Update(payment);
            order.Status = "Cancelled";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> PurchasedProductForReview(int userId, ReviewCreationDto reviewDTO)
        {
            var hasPurchasedProduct = await _context.Orders
                      .Include(o => o.OrderItems)
                      .AnyAsync(o => o.UserID == userId && o.OrderItems.Any(oi => oi.ProductID == reviewDTO.ProductID));
            return hasPurchasedProduct;
        }
    }
}
