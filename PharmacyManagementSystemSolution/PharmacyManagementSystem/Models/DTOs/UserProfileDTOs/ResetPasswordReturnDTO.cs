using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.UserProfileDTOs
{
    public class ResetPasswordReturnDTO
    {
        [Required]
        public int UserId { get; set; }
        
        
     
        [Required]
        public string Role { get; set; }
    }
}