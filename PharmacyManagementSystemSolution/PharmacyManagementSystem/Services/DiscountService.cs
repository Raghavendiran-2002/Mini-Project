using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.DiscountDTOs;

namespace PharmacyManagementSystem.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<int, Discount> _discountRepo;

        public DiscountService(IRepository<int , Discount> discountRepo)
        {
           _discountRepo = discountRepo;
        }

        public async Task<Discount> AddDiscount(AddDiscountDTO addDiscountDTO)
        {
            Discount discount = MapAddDiscountToDiscount(addDiscountDTO);
            discount = await _discountRepo.Add(discount);
            return discount;
        }

        private Discount MapAddDiscountToDiscount(AddDiscountDTO addDiscountDTO)
        {
            Discount discount = new Discount();
            discount.DiscountName = addDiscountDTO.DiscountName;
            discount.DiscountPercentage = addDiscountDTO.DiscountPercentage;
            discount.StartDate = addDiscountDTO.StartDate;
            discount.EndDate = addDiscountDTO.EndDate;
            return discount;
        }

        public async Task<Discount> DeleteDiscount(int id)
        {
            Discount discount = await _discountRepo.Delete(id);
            return discount;
        }

        public async Task<Discount> GetDiscountById(int id)
        {
           Discount discount = await _discountRepo.Get(id);
            return discount;
        }

        public async Task<IEnumerable<Discount>> GetDiscounts()
        {
            var discounts = await _discountRepo.Get();
            return discounts;
        }

        public async Task<Discount> UpdateDiscount(UpdateDiscountDTO updateDiscountDTO)
        {
            Discount discount = await _discountRepo.Get(updateDiscountDTO.DiscountID);
            discount = MapUpdateDiscountToDiscount(updateDiscountDTO, discount);
            discount = await _discountRepo.Update(discount);
            return discount;
        }

        private Discount MapUpdateDiscountToDiscount(UpdateDiscountDTO updateDiscountDTO, Discount discount)
        {            
            discount.DiscountID = updateDiscountDTO.DiscountID;
            discount.DiscountName = updateDiscountDTO.DiscountName;
            discount.DiscountPercentage = updateDiscountDTO.DiscountPercentage;
            discount.StartDate = updateDiscountDTO.StartDate;
            discount.EndDate = updateDiscountDTO.EndDate;
            return discount;
        }
    }
}
