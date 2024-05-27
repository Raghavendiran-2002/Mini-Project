using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.OrderDTOs
{
    public class OrderCreationDto
    {
        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}