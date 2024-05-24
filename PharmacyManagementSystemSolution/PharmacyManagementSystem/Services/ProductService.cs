using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;

namespace PharmacyManagementSystem.Services
{
    public class ProductService: IProductService
    {
        private readonly IRepository<int, Product> _productRepo;

        public ProductService(IRepository<int , Product> productRepo)
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
            return product;
        }

        public Task<Product> DeleteProduct(int productId)
        {
            var product = _productRepo.Delete(productId);
            return product;
        }

        public Task<Product> GetProduct(int productId)
        {
           var product = _productRepo.Get(productId);
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
    }
}
