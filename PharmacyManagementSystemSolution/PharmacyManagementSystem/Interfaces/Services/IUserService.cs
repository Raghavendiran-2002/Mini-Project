using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(LoginDTO loginDTO);
        public Task<RegisterReturnDTO> Register(RegisterDTO employeeDTO);

    }
}
