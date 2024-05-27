using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class OrderRepository : BaseRepository<int , Order> , IRepository<int ,Order>
    {
        public OrderRepository(DBPharmacyContext context) : base(context) 
        {
            
        }
    }
}
