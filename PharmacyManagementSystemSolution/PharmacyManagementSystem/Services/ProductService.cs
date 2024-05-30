using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;
using PharmacyManagementSystem.Models.Repositories;

namespace PharmacyManagementSystem.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository<int, Product> _productRepo;

        public ProductService(IProductRepository<int, Product> productRepo)
        {
            _productRepo = productRepo;            
        }

        public async Task<Product> AddProduct(AddProductDTO productDTO)
        {             
            Product product = await _productRepo.Add(MapAddProductDTOToProduct(productDTO));
            return product;            
        }

        private Product MapAddProductDTOToProduct(AddProductDTO productDTO)
        {
            Product product = new Product();;
            product.ProductName = productDTO.ProductName;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;      
            product.ImageUrl = productDTO.ImageUrl;
            product.CategoryID = productDTO.CategoryID;
            product.DiscountID = productDTO.DiscountID;
            return product;
        }

        public Task<Product> DeleteProduct(int productId)
        {
            var product = _productRepo.Delete(productId);
            if (product == null)
                throw new ProductNotFound("Product Not Found");
            return product;
        }

        public Task<Product> GetProduct(int productId)
        {
           var product = _productRepo.Get(productId);
            if (product == null)
                throw new ProductNotFound("Product Not Found");
           return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productRepo.Get();
            return products;
        }

        public async Task<Product> UpdateProduct(UpdateProductDTO productDTO)
        {
            Product product = await _productRepo.Get(productDTO.ProductID);
            if (product == null)
                throw new ProductNotFound("Product Not Found");
            product = await _productRepo.Update(MapUpdateProductDTOToProduct(product,productDTO));                 
            return product;
        }

        private Product MapUpdateProductDTOToProduct(Product product,UpdateProductDTO productDTO)
        {            
            product.ProductID = productDTO.ProductID;
            product.ProductName = productDTO.ProductName;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.ImageUrl = productDTO.ImageUrl;
            return product;
        }

        public Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId)
        {
            var products = _productRepo.GetProductByCategory(categoryId);
            return products;
        }

        public Task<IEnumerable<Product>> GetProductBasedOnAvailability()
        {
            var products = _productRepo.GetAvailableProducts();
            return products;
        }

        public Task<IEnumerable<Product>> GetProductByName(string name)
        {
           var products = _productRepo.GetProductByName(name);
            return products; 
        }

        public Task<IEnumerable<Product>> GetProductsByPriceRange(int startPrice, int endPrice)
        {
            var products = _productRepo.GetProductsByPriceRange(startPrice, endPrice);
            return products;
        }
    }
}
