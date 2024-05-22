using EmployeeRequestTrackerAPI.Models.DTOs;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(LoginDTO loginDTO);
        public Task<RegisterReturnDTO> Register(RegisterDTO employeeDTO);

    }
}
