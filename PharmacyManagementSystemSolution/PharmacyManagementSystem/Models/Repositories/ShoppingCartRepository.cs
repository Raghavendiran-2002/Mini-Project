using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ShoppingCartRepository : BaseRepository<int , ShoppingCart> , IShoppingCartRepository<int , ShoppingCart>    {
        public ShoppingCartRepository(DBPharmacyContext context) : base(context)
        {
            
        }
        public async override Task<ShoppingCart> Get(int key)
        {

            var item = await _context.ShoppingCarts.Include(c => c.ShoppingCartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserID == key);
            return item;
        }
        public override async Task<IEnumerable<ShoppingCart>> Get()
        {
            var items = await _context.ShoppingCarts.Include(c => c.ShoppingCartItems).ThenInclude(i => i.Product).ToListAsync();
            return items;
        }
    }
}
