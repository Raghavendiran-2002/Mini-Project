using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ReviewRepository : BaseRepository<int , Review> , IRepository<int, Review>

    {
        public ReviewRepository(DBPharmacyContext context) : base(context)
        {
            
        }
    }
}
