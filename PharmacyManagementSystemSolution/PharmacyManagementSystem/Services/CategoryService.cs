using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;

namespace PharmacyManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<int, Category> _categoryRepo;

        public CategoryService(IRepository<int, Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<Category> AddCategory(AddCategoryDTO category)
        {
            Category c = MapAddCategoryDTOToCategory(category);
            var addedCategory = await _categoryRepo.Add(c);
            return addedCategory;
        }

        private Category MapAddCategoryDTOToCategory(AddCategoryDTO categoryDTO)
        {
            Category category = new Category();
            category.CategoryName = categoryDTO.CategoryName;
            category.Description = categoryDTO.Description;
            category.ImageUrl = categoryDTO.ImageUrl;
            return category;
        }

        public async Task<Category> DeleteCategory(int Id)
        {
            var deleteCategory = await _categoryRepo.Delete(Id);
            return deleteCategory;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var allCategories = await _categoryRepo.Get();
            return allCategories;
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            var getCategory = await _categoryRepo.Get(Id);
            return getCategory; 
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDTO category)
        {
            Category updatedCategory = MapUpdateCategoryDTOToCategory(category);
            await _categoryRepo.Update(updatedCategory);
            return updatedCategory;
        }

        private Category MapUpdateCategoryDTOToCategory(UpdateCategoryDTO categoryDTO)
        {
            Category category = new Category();
            category.CategoryID = categoryDTO.CategoryId;
            category.CategoryName = categoryDTO.CategoryName;
            category.Description = categoryDTO.Description;
            category.ImageUrl = categoryDTO.ImageUrl;
            return category;
        }
    }
}
