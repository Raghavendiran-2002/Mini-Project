using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Product id cannot be empty")] 
        public int ProductID { get; set; }
        [Required, MaxLength(100)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be greater than 0")]
        public int Stock { get; set; }
       
        public string ImageUrl { get; set; } = string.Empty;
    }
}