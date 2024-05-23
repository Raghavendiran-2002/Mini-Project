using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        public string Address { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
