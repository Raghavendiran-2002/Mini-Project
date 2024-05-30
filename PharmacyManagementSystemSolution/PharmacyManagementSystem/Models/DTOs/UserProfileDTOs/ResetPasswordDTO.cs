using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models.DTOs.UserProfileDTOs
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "User id cannot be empty")]
        public int UserId { get; set; }

        [MinLength(6, ErrorMessage = "Old Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string PreviousPassword { get; set; }

        [MinLength(6, ErrorMessage = "New Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string NewPassword { get; set; }
        
      
    }
}