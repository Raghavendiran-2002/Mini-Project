using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class CategoryRepository : BaseRepository<int ,Category>, IRepository<int , Category>
    {
        public CategoryRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
