using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.PaymentDTOs
{
    public class PaymentCreationDto
    {
        [Required(ErrorMessage = "Order id cannot be empty")]
        public int OrderID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}