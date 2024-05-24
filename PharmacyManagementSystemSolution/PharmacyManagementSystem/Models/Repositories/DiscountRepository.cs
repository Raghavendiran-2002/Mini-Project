using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class DiscountRepository : BaseRepository<int , Discount>, IRepository<int, Discount>
    {
        public DiscountRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
