using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
