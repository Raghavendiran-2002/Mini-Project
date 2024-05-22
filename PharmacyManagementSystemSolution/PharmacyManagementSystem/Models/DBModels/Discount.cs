using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }
        [Required, MaxLength(50)]
        public string DiscountName { get; set; }
        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; }
    }
}
