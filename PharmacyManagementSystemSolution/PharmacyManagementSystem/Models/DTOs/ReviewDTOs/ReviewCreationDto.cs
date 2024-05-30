using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.ReviewDTOs
{
    public class ReviewCreationDto
    {
        [Required(ErrorMessage = "Product id cannot be empty")]
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}