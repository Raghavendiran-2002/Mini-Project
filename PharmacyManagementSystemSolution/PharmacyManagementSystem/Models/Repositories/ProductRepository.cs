using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ProductRepository : BaseRepository<int , Product>, IRepository<int, Product>
    {
        public ProductRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
