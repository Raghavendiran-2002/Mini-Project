using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.Payments;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.PaymentDTOs;

namespace PharmacyManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository<int, Payment> _paymentRepo;
        private readonly IOrderRepository<int, Order> _orderRepo;

        public PaymentService(IPaymentRepository<int, Payment> paymentRepo, IOrderRepository<int, Order> orderRepo)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
        }
        public async Task<Payment> CreatePayment(int userId, PaymentCreationDto paymentDto)
        {

            var order = await _orderRepo.Get(paymentDto.OrderID);
            var prevPayment = (await _paymentRepo.Get()).FirstOrDefault(p => p.OrderID == paymentDto.OrderID);
            if (prevPayment != null)
            {
                if (prevPayment.OrderID == paymentDto.OrderID && paymentDto.Amount > order.TotalAmount)
                    await _paymentRepo.Delete(prevPayment.PaymentID);
            }
            if (order == null || order.UserID != userId)
            {
                throw new OrderNotFound("Order not found or unauthorized access.");
            }


            if (order.TotalAmount > paymentDto.Amount)
                throw new PaymentNotSuccessfull("Payment not successfull");

            if (order.TotalAmount <= paymentDto.Amount)
            {
                order.Status = "Paid";
                await _orderRepo.Update(order);
            }

            var payment = new Payment
            {
                OrderID = paymentDto.OrderID,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                Status = "Paid"
            };
            payment = await _paymentRepo.Add(payment);

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
                throw new OrderNotFound("Order not found or unauthorized access.");
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
            if (orders == null)
                throw new NoOrderFound($"No Order found with user Id : {userId}");

            var payment = (await _paymentRepo.Get())
                                 .Where(p => orders.Contains(p.OrderID))
                                 .ToList();
            return payment;
        }
    }
}
