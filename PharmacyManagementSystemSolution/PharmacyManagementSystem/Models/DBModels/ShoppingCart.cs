using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class ShoppingCart
    {
        [Key]
        public int CartID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
