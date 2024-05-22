using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
        public string ShippingAddress { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
