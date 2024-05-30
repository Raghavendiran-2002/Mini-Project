using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.PaymentDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<Payment> CreatePayment(int userId, PaymentCreationDto paymentDto);

        public Task<IEnumerable<Payment>> GetPaymentsByOrder(int userId, int orderId);
        public Task<IEnumerable<Payment>> GetAllPayments();
        public Task<IEnumerable<Payment>> GetPaymentsByUser(int userId);


    }
}
