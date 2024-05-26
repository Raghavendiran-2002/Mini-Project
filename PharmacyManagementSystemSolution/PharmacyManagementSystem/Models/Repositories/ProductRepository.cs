using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.Products;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Models.DBModels;
using System.Xml.Linq;

namespace PharmacyManagementSystem.Models.Repositories
{
    public class ProductRepository : BaseRepository<int , Product>, IProductRepository<int, Product>
    {
        public ProductRepository(DBPharmacyContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            var products = await _context.Products.Where(p=>p.Stock > 0).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int categoryId)
        {
            var products = await _context.Products.Where(p=>p.CategoryID == categoryId).ToListAsync();
            if (products.Count == 0)
                throw new NoProductFoundByName($"No product found by in Category {categoryId}");
            return products;
        }

        public async Task <IEnumerable< Product>> GetProductByName(string name)
        {
            var products = await _context.Products.Where(p=>p.ProductName == name).ToListAsync();
            if (products.Count == 0)
                throw new NoProductFoundByName($"No product found by name {name}");
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(int startPriceRange, int endPriceRange)
        {
            var products = await _context.Products.Where(p=>p.Price > startPriceRange).Where(p=>p.Price < endPriceRange).ToListAsync();
            if (products.Count == 0)
                throw new NoProductFoundByName($"No product found by price between {startPriceRange} and {endPriceRange}");
            return products;
        }
    }
}
