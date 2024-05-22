using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DBModels
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }
    }
}
