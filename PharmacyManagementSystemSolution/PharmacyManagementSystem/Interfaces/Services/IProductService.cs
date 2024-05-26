using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int productId);
        public Task<Product> DeleteProduct(int productId);
        public Task<Product> AddProduct(AddProductDTO product);
        public Task<Product> UpdateProduct(UpdateProductDTO product);
        public Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId);

        public Task<IEnumerable<Product>> GetProductBasedOnAvailability();
        public Task<IEnumerable<Product>> GetProductByName(string name);
        public Task<IEnumerable<Product>> GetProductsByPriceRange(int startPrice, int endPrice);
    }
}
