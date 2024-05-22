using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        public string Address { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; } = "Customer";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
