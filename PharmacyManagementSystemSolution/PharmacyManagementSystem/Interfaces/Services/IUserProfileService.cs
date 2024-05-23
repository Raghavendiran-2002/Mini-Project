using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IUserProfileService
    {
        public Task<UserProfileReturnDTO> ViewProfile(int Id);
        public Task<UserProfileReturnDTO> UpdateProfile(UpdateUserDTO updatedUser);
        public Task<ResetPasswordReturnDTO> ResetPassword(ResetPasswordDTO resetPasswordReturn);

        public Task<UserProfileReturnDTO>DeleteProfile(int Id);
    }
}
