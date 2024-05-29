using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Exceptions.Payments;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.PaymentDTOs;

namespace PharmacyManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<int, Payment> _paymentRepo;
        private readonly IOrderRepository<int, Order> _orderRepo;

        public PaymentService(IRepository<int, Payment> paymentRepo, IOrderRepository<int , Order> orderRepo)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
        }

        public  async Task<Payment> CreatePayment(int userId, PaymentCreationDto paymentDto)
        {
            var order = await _orderRepo.Get(paymentDto.OrderID);
            if (order == null || order.UserID != userId)
            {
                throw new OrderNotFound("Order not found or unauthorized access.");
            }

            var payment = new Payment
            {
                OrderID = paymentDto.OrderID,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
            };

            payment = await _paymentRepo.Add(payment);
            

            if (order.TotalAmount <= paymentDto.Amount)
            {
                order.Status = "Paid";
                await _orderRepo.Update(order);                
            }

            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            var payments = await _paymentRepo.Get();
            return payments;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrder(int userId, int orderId)
        {
            var order = await _orderRepo.Get(orderId);
            if (order == null || order.UserID != userId)
            {
                throw new OrderNotFound("Order not found or unauthorized access.");
            }

            return (await _paymentRepo.Get())
                                 .Where(p => p.OrderID == orderId)
                                 .ToList();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUser(int userId)
        {
            var orders = (await _orderRepo.Get())
                                .Where(o => o.UserID == userId)
                                .Select(o => o.OrderID)
                                .ToList();
            var payment = (await _paymentRepo.Get())
                                 .Where(p => orders.Contains(p.OrderID))
                                 .ToList();
            return payment;
        }
    }
}
