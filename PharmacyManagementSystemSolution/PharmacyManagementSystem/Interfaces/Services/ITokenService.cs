using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Interfaces.Services
{

   
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
