using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Services
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Category id cannot be empty")]
        public int CategoryId { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}