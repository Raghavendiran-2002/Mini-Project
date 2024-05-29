using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.ReviewDTOs
{
    public class ReviewDto
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}