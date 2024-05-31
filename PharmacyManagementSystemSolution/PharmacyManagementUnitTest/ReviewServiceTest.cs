using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.Products;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public class ReviewServiceTest
    {
        DBPharmacyContext context;
        IProductRepository<int, Product> product;
        IProductService productService;
        IReviewRepository<int, Review> review;
        IReviewService reviewService;
        IOrderRepository<int, Order> order;
        IOrderService orderService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("DBReviewTest");
            context = new DBPharmacyContext(optionsBuilder.Options);
            order = new OrderRepository(context);
            review = new ReviewRepository(context);
            product = new ProductRepository(context);
            product.Add(new Product() { ProductID = 1, ProductName = "meds", Description = "meds are usefull", Price = 40, Stock = 5, CategoryID = 1, DiscountID = 1 });
            product.Add(new Product() { ProductID = 2, ProductName = "meds", Description = "meds are usefull", Price = 40, Stock = 5, CategoryID = 1, DiscountID = 1 });
            order.Add(new Order() { OrderID = 5, ShippingAddress = " ", UserID = 1, Status = "xcb", TotalAmount = 577 });
            review.Add(new Review() { ReviewID = 1, ProductID = 2, Comment = "gfdg", Rating = 5, UserID = 1 });
            reviewService = new ReviewService(review, product,order);
            orderService = new OrderService(order, product);
            product = new ProductRepository(context);
            productService = new ProductService(product);
            
        }
        [Test]
        public async Task AddReview()
        {
            // Arrange

            // Action
            var result =  reviewService.AddReview(1, new ReviewCreationDto() { Comment = "dfhgdh", ProductID = 1, Rating = 6 });
            // Assert
            var ex = Assert.ThrowsAsync<ProductNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Product not found."));
        }
        [Test]
        public async Task AddReviewFailed()
        {
            // Arrange

            // Action
            var result = reviewService.AddReview(2, new ReviewCreationDto() { Comment = "dfhgdh", ProductID = 2, Rating = 6 });
            // Assert
            var ex = Assert.ThrowsAsync<ProductNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Product not found."));
        }
        [Test]
        public async Task GetReviewsForProductTest()
        {
            // Arrange

            // Action
            var result = await  reviewService.GetReviewsForProduct(2);
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
