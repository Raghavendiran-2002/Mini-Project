using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public class UpdateProductDTO
    {
        public int ProductID { get; set; }
        [Required, MaxLength(100)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
       
        public string ImageUrl { get; set; } = string.Empty;
    }
}