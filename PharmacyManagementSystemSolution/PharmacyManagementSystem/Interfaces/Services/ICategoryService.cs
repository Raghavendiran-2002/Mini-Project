using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int Id);
        public Task<Category> AddCategory(AddCategoryDTO category);
        public Task<Category> DeleteCategory(int Id);
        public Task<Category> UpdateCategory(UpdateCategoryDTO category);
    }
}

