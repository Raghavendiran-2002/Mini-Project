using PharmacyManagementSystem.Models.DBModels;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.CategoryDTOs
{
    public class AddCategoryDTO
    {
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
     
    }
}