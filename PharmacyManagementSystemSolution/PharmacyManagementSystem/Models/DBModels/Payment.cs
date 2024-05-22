using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
