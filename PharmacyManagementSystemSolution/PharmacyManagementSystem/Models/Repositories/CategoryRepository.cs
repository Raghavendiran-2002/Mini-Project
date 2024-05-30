using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class CategoryRepository : BaseRepository<int ,Category>, ICategoryRepository<int , Category>
    {
        public CategoryRepository(DBPharmacyContext context) : base(context)
        {
            
        }
        public override async Task<IEnumerable<Category>> Get()
        {
            var categories = await _context.Categories.Include(p => p.Products).ToListAsync();
            return categories;
        }
        public override async Task<Category> Get(int key)
        {
            var category = await _context.Categories.Include(p => p.Products).FirstAsync(c => c.CategoryID == key);
            return category;
        }
    }
}
