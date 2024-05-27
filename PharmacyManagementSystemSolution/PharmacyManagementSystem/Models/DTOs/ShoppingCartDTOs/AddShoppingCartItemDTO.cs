using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs
{
    public class AddShoppingCartItemDTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}