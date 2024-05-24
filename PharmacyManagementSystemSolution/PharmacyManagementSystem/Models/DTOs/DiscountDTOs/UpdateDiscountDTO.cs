using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.DiscountDTOs
{
    public class UpdateDiscountDTO
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
    }
}