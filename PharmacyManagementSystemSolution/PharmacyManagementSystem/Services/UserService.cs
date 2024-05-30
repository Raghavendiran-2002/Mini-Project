

using PharmacyManagementSystem.Exceptions.Auth;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyManagementSystem.Interfaces.Services

{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IShoppingCartRepository<int, ShoppingCart> _cartRepo;

        public UserService(IRepository<int , User> userRepo,IShoppingCartRepository<int, ShoppingCart> cartRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _cartRepo = cartRepo;
        }
        public async Task<LoginReturnDTO> Login(LoginDTO user)
        {
            var userDB = await _userRepo.Get(user.UserId);
            if(userDB == null)
            {
                throw new UnauthorizedAccessException("Invalid username and password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHash);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(userDB);
                return loginReturnDTO;
            }
            throw new UnauthorizedAccessException("Invalid username and password");
        }
        private LoginReturnDTO MapUserToLoginReturn(User user)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserID = user.UserID;
            returnDTO.Role = user.Role ?? "User";
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
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

        public async Task<RegisterReturnDTO> Register(RegisterDTO newUser)
        {
            User user = null;
            try
            {
                user = MapRegisterDTOToUser(newUser);
                user = await _userRepo.Add(user);              
                RegisterReturnDTO registerReturnDTO = MapUserToRegisterReturnDTO(user);
                var cart =  await _cartRepo.Add(new ShoppingCart() { UserID = registerReturnDTO.UserId });
                if(cart == null)
                {
                    RolebackUser(user.UserID);
                    throw new UserNotRegistered("User not Registered");
                }
                return registerReturnDTO;
            }
            catch (Exception ) {}
            throw new UnauthorizedAccessException("Invalid username and password");
        }

        private async void RolebackUser(int userID)
        {
            await _userRepo.Delete(userID);            
        }

        private  RegisterReturnDTO MapUserToRegisterReturnDTO(User user)
        {   
            RegisterReturnDTO returnDTO = new RegisterReturnDTO();
            returnDTO.UserId = user.UserID;
            returnDTO.Username = user.Username;
            returnDTO.Email = user.Email;
            returnDTO.FullName = user.FullName;
            returnDTO.Address = user.Address;
            returnDTO.PhoneNumber = user.PhoneNumber;
            returnDTO.Role = user.Role;
            return returnDTO;

        }

        private User MapRegisterDTOToUser(RegisterDTO newUser)
        {
            User user = new User();
            user.Username = newUser.Username;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHash = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password));
            user.Email = newUser.Email;
            user.FullName = newUser.FullName;
            user.Address = newUser.Address;
            user.PhoneNumber = newUser.PhoneNumber;
            user.Role = newUser.Role;
            return user;
        }
    }
}
