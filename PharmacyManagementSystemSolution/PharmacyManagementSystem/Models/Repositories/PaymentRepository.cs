using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class PaymentRepository : BaseRepository<int , Payment> , IRepository<int , Payment>
    {
        public PaymentRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
