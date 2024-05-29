using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.PaymentDTOs
{
    public class PaymentCreationDto
    {
        [Required]
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}