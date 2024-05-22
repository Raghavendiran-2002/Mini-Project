using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class UserRepository : BaseRepository<int , User>, IRepository<int , User>
    {
        public UserRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
