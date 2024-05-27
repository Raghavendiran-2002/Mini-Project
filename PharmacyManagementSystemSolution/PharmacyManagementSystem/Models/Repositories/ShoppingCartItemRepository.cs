using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ShoppingCartItemRepository : BaseRepository<int , ShoppingCartItem> , IRepository<int ,ShoppingCartItem>    {
        public ShoppingCartItemRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
