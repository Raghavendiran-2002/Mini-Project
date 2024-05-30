using PharmacyManagementSystem.Models.DBModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        [Required, MaxLength(100)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than 0")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Category id cannot be empty")]
        public int CategoryID { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Discount id cannot be empty")]
        public int DiscountID { get; set; }
    }
}