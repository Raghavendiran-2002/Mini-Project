using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyManagementSystem.Exceptions.UserProfile;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyManagementSystem.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<int, User> _userRepo;

        public UserProfileService(IRepository<int, User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<ResetPasswordReturnDTO> ResetPassword(ResetPasswordDTO passwordDTO)
        {
            var user = await _userRepo.Get(passwordDTO.UserId);
            if (user == null)
                throw new NoUserFound($"User not found with Id : {passwordDTO.UserId}");
            ResetPasswordUser(user,passwordDTO.PreviousPassword, passwordDTO.NewPassword);
            await _userRepo.Update(user);
            ResetPasswordReturnDTO resetPasswordDTO = new ResetPasswordReturnDTO() { UserId = user.UserID, Role = user.Role };
            return resetPasswordDTO;
        }

        private void ResetPasswordUser(User user, string previousPassword, string newPassword)
        {
            HMACSHA512 hMACSHA = new HMACSHA512(user.PasswordHash);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(previousPassword));
            bool isPasswordSame = ComparePassword(encrypterPass, user.Password);
            if (!isPasswordSame)
                throw new PasswordIncorrect("Invalid Password");
            HMACSHA512 newhMACSHA = new HMACSHA512();
            user.PasswordHash = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
        }
        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<UserProfileReturnDTO> UpdateProfile(UpdateUserDTO updatedUser)
        {
            var user =  await _userRepo.Get(updatedUser.UserId);
            if (user == null)
                throw new NoUserFound($"User not found with Id : {updatedUser.UserId}");
            var updateUser = MapUpdateUserDTOToUser(user, updatedUser);
            user = await _userRepo.Update(updateUser);
            return MapUserToUserProfileReturnDTO(user);
        }

        private User MapUpdateUserDTOToUser(User user , UpdateUserDTO updatedUser)
        {           
            user.UserID = updatedUser.UserId;
            user.Address = updatedUser.Address;
            user.Username = updatedUser.Username;
            user.FullName = updatedUser.FullName;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;
            return user;
        }

        public async Task<UserProfileReturnDTO> ViewProfile(int Id)
        {
            var user = await _userRepo.Get(Id);
            if (user == null)
                throw new NoUserFound($"User not found with Id : {Id}");
            UserProfileReturnDTO profile = MapUserToUserProfileReturnDTO(user);
            return profile;
        }

        private UserProfileReturnDTO MapUserToUserProfileReturnDTO(User user)
        {
            UserProfileReturnDTO userProfileDTO = new UserProfileReturnDTO();
            userProfileDTO.Username = user.Username;
            userProfileDTO.Role = user.Role;
            userProfileDTO.Email = user.Email;
            userProfileDTO.UserId = user.UserID;
            userProfileDTO.FullName = user.FullName;
            userProfileDTO.Address = user.Address;
            userProfileDTO.PhoneNumber = user.PhoneNumber;
            return userProfileDTO;
        }

        public async Task<UserProfileReturnDTO> DeleteProfile(int Id)
        {
            var user = await _userRepo.Get(Id);
            if (user == null)
                throw new NoUserFound($"User not found with Id : {Id}");
            await _userRepo.Delete(Id);
            UserProfileReturnDTO profile = MapUserToUserProfileReturnDTO(user);
            return profile;
        }
    }
}
