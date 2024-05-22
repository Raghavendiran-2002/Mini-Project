using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
