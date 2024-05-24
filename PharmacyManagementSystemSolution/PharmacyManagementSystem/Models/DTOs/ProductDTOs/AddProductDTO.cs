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
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public int? CategoryID { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int? DiscountID { get; set; }
    }
}