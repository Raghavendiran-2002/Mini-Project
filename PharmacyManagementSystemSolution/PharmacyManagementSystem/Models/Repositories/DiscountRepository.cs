using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class DiscountRepository : BaseRepository<int , Discount>, IDiscountRepository<int, Discount>
    {
        public DiscountRepository(DBPharmacyContext context) : base(context)
        {
            
        }
        public  async override Task<IEnumerable<Discount>> Get()
        {
            var discounts = await _context.Discounts.Include(p => p.Products).ToListAsync();
            return discounts;
        }
        public override async Task<Discount> Get(int key)
        {
            var discount = await _context.Discounts.Include(p=>p.Products).FirstAsync(d=>d.DiscountID == key);
            return discount;
        }
    }
}
