using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.UserProfileDTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string PreviousPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        
      
    }
}