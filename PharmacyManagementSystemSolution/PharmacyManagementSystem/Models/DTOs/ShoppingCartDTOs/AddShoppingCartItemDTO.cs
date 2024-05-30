using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs
{
    public class AddShoppingCartItemDTO
    {
        
        [Required(ErrorMessage = "User id cannot be empty")]
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Cart id cannot be empty")]
        public int CartID { get; set; }

        [Required(ErrorMessage = "Product id cannot be empty")]
        public int ProductID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}