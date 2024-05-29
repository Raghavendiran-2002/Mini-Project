using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);
        public Task<Review> AddReviewAsync(int userId, ReviewCreationDto reviewDto);

    }
}
