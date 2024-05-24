using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.DiscountDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IDiscountService
    {
        public Task<Discount> AddDiscount(AddDiscountDTO addDiscountDTO);
        public Task<Discount> GetDiscountById(int id);
        public Task<IEnumerable<Discount>> GetDiscounts();
        public Task<Discount> UpdateDiscount(UpdateDiscountDTO updateDiscountDTO);
        public Task<Discount> DeleteDiscount(int id);
    }
}
