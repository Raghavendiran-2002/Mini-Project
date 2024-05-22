using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTrackerAPI.Models.DTOs
{
    public class RegisterReturnDTO
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
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
