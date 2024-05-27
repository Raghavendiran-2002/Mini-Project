using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ShoppingCartRepository : BaseRepository<int , ShoppingCart> , IRepository<int , ShoppingCart>    {
        public ShoppingCartRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
