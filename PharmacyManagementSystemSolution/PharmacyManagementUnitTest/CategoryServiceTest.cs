using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public class CategoryServiceTest
    {
        DBPharmacyContext context;
        ICategoryRepository<int, Category> category;
        ICategoryService categoryService;
        [SetUp]
        public void Setup() {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("dummyDsB");
            context = new DBPharmacyContext(optionsBuilder.Options);
            category = new CategoryRepository(context);
            categoryService = new CategoryService(category);
            category.Add(new Category() { CategoryID = 1, CategoryName = "Meds", Description = "Meds are best", ImageUrl = "" });
            category.Add(new Category() { CategoryID = 2, CategoryName = "Meds - Meds", Description = "Meds are best / worst", ImageUrl = "" });
        }
        [Test]
        public async Task AddCategorySucessfull()
        {
            // Arrange                              
            AddCategoryDTO categoryDTO = new AddCategoryDTO() { CategoryName = "", Description = "", ImageUrl = "" };
            // Action
            var result = await categoryService.AddCategory(categoryDTO);
            // Assert
            Assert.That(result.CategoryID, Is.EqualTo(2));
        }
        [Test]
        public async Task GetCategoryByIdSucessfull()
        {
            // Arrange                              

            // Action
            var result = await categoryService.GetCategoryById(1);
            // Assert
            Assert.That(result.CategoryName, Is.EqualTo("Meds"));
        }
        [Test]
        public async Task GetAllCategorySucessfull()
        {
            // Arrange                              

            // Action
            var result = await categoryService.GetAllCategories();
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task UpdateCategorySucessfull()
        {
            // Arrange                              
            UpdateCategoryDTO categoryDTO = new UpdateCategoryDTO() {CategoryId= 1, CategoryName = "Meds", Description = "Meds are best", ImageUrl = "url.com" };
            // Action
            var result = await categoryService.UpdateCategory(categoryDTO);
            // Assert
            Assert.That(result.CategoryID, Is.EqualTo(1));
        }
        [Test]
        public async Task DeleteCategorySucessfull()
        {
            // Arrange                              
            
            // Action
            var result = await categoryService.DeleteCategory(2);
            // Assert
            Assert.That(result.CategoryID, Is.EqualTo(2));
        }
    }
}
