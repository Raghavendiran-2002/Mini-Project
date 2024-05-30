using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDto>> GetReviewsForProduct(int productId);
        public Task<Review> AddReview(int userId, ReviewCreationDto reviewDto);

    }
}
