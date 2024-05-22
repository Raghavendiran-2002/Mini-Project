using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required, MaxLength(100)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public int? CategoryID { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public int? DiscountID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        [ForeignKey("DiscountID")]
        public Discount Discount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
