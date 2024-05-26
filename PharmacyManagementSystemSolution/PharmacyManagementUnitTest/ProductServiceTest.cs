using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.Products;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.DiscountDTOs;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public  class ProductServiceTest
    {
        DBPharmacyContext context;
        IProductRepository<int, Product> product;
        IProductService productService;
        IRepository<int, Discount> discount;
        IDiscountService discountService;
        IRepository<int, Category> category;
        ICategoryService categoryService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("dummyDBProductTest");
            context = new DBPharmacyContext(optionsBuilder.Options);
            category = new CategoryRepository(context);
            categoryService = new CategoryService(category);
            category.Add(new Category() { CategoryID = 1, CategoryName = "Meds", Description = "Meds are best", ImageUrl = "" });
            discount = new DiscountRepository(context);
            discountService = new DiscountService(discount);
            discount.Add(new Discount() {DiscountID = 1, DiscountName = "Bank", DiscountPercentage = 10, StartDate = new DateTime(2024, 5, 15), EndDate = new DateTime(2024, 6, 8) });
            product = new ProductRepository(context);
            productService = new ProductService(product);
            product.Add(new Product() { ProductID = 1, ProductName = "meds", Description = "meds are usefull", Price = 40, Stock = 5, CategoryID = 1, DiscountID = 1 });
        }
        [Test]
        public async Task AddProduct()
        {
            // Arrange
            AddProductDTO product = new AddProductDTO() {ProductName="Dolo", Description="Fever", Price=50,Stock=30,CategoryID=1,DiscountID = 1 };
            // Action
            var result = await productService.AddProduct(product);
            // Assert
            Assert.That(result.ProductID, Is.EqualTo(2));
        }
        [Test]
        public async Task GetProduct()
        {
            // Arrange
            
            // Action
            var result = await productService.GetProduct(1);
            // Assert
            Assert.That(result.ProductID, Is.EqualTo(1));
        }
        [Test]
        public async Task GetProducts()
        {
            // Arrange

            // Action
            var result = await productService.GetProducts();
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task DeleteProduct()
        {
            // Arrange
            AddProductDTO product = new AddProductDTO() { ProductName = "new Meds", Description = "Dia", Price = 500, Stock = 30, CategoryID = 1, DiscountID = 1 };      
            await productService.AddProduct(product);
            // Action
            var result = await productService.GetProduct(2);
            // Assert
            Assert.That(result.ProductID, Is.EqualTo(2));
        }
        [Test]
        public async Task UpdateProducts()
        {
            // Arrange
            UpdateProductDTO prod = new UpdateProductDTO() { ProductID = 1, ProductName ="painkiller", Description ="good", Price = 80, Stock = 5, ImageUrl =" " };
            // Action
            var result = await productService.UpdateProduct(prod);
            // Assert
            Assert.That(result.ProductName, Is.EqualTo("painkiller"));
        }
        [Test]
        public async Task GetProductsBasedOnAvailablitiy()
        {
            // Arrange

            // Action
            var result = await productService.GetProductBasedOnAvailability();
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task GetProductByName()
        {
            // Arrange

            // Action
            var result = await productService.GetProductByName("meds");
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task GetProductByPriceRangeSuccess()
        {
            // Arrange

            // Action
            var result = await productService.GetProductsByPriceRange(1, 60);
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task GetProductByPriceRangeFailed()
        {
            // Arrange

            // Action
            var result = productService.GetProductsByPriceRange(1, 10);
            // Assert
            var ex = Assert.ThrowsAsync<NoProductFoundByName>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"No product found by price between 1 and 10"));
        }
        [Test]
        public async Task GetProductByCategoryId()
        {
            // Arrange

            // Action
            var result = await productService.GetProductByCategoryId(1);
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
