using PharmacyManagementSystem.Exceptions.Category;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;

namespace PharmacyManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository<int, Category> _categoryRepo;

        public CategoryService(ICategoryRepository<int, Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<Category> AddCategory(AddCategoryDTO categoryDTO)
        {
            Category category = MapAddCategoryDTOToCategory(categoryDTO);
            category = await _categoryRepo.Add(category);
            return category;
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
            var deleteCategory =await _categoryRepo.Delete(Id);
            if (deleteCategory == null)
                throw new NoCategoryFound("No Category Found");
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
            if (getCategory == null)
                throw new NoCategoryFound("No Category Found");
            return getCategory; 
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDTO category)
        {
            Category updateCategory = MapUpdateCategoryDTOToCategory(category);
            updateCategory = await _categoryRepo.Update(updateCategory);
            if (updateCategory == null)
                throw new NoCategoryFound("No Category Found");
            return updateCategory;
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
