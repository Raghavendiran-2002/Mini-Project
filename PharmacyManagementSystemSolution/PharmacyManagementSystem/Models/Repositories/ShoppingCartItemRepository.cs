using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ShoppingCartItemRepository : BaseRepository<int , ShoppingCartItem> , IShoppingCartItemsRepository<int ,ShoppingCartItem>    {
        public ShoppingCartItemRepository(DBPharmacyContext context) : base(context)
        {
            
        }
        public override async Task<IEnumerable<ShoppingCartItem>> Get()
        {
            var shoppingCartItems = await _context.ShoppingCartItems.Include(p => p.Product).Include(c => c.ShoppingCart).ToListAsync();
            return shoppingCartItems;
        }
        public override async Task<ShoppingCartItem> Get(int key)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.Include(p => p.Product).Include(c => c.ShoppingCart).FirstAsync(s => s.CartItemID == key) ;
            return shoppingCartItem;
        }
    }
}
