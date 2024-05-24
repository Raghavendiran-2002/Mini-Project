using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Services
{
    public class UpdateCategoryDTO
    {
        [Required]
        public int CategoryId { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}