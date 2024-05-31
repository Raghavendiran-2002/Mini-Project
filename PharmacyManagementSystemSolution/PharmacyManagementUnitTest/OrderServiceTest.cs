using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.UserProfile;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public class OrderServiceTest
    {
        DBPharmacyContext context;
        IOrderRepository<int, Order> order;
        IOrderService orderService;
        IProductRepository<int, Product> product;
        IDiscountRepository<int, Discount> discount;
        ICategoryRepository<int, Category> category;
        IProductService productService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("DBProductTest");
            context = new DBPharmacyContext(optionsBuilder.Options);
            order = new OrderRepository(context);
            category = new CategoryRepository(context);
            discount = new DiscountRepository(context);
            product = new ProductRepository(context);
            order.Add(new Order() { OrderID = 1, UserID = 1, TotalAmount = 334, Status = "pending" ,ShippingAddress = "gfdhh"});
            order.Add(new Order() { OrderID = 2, UserID = 1, TotalAmount = 334, Status = "pending" , ShippingAddress = "dhjrfju"});
            product.Add(new Product() { ProductID = 1, ProductName = "meds", Description = "meds are usefull", Price = 40, Stock = 5, CategoryID = 1, DiscountID = 1 });
            orderService = new OrderService(order, product);            
        }
        [Test]
        public async Task GetOrderById()
        {
            // Arrange                              

            // Action
            var result = await orderService.GetOrderById(1);
            // Assert
            Assert.That(result.OrderID, Is.EqualTo(1));
        }
        [Test]
        public async Task GetOrderByIdNotFound()
        {
            // Arrange                              

            // Action
            var result = orderService.GetOrderById(10);
            // Assert
            var ex = Assert.ThrowsAsync<NoOrderFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"No Order found with Id : 10"));
        }
        [Test]
        public async Task GetUserOrders()
        {
            // Arrange                              

            // Action
            var result = await orderService.GetUserOrders(1);
            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        [Test]
        public async Task PlaceOrderProductNotFound()
        {
            // Arrange                              

            // Action
            var result = orderService.PlaceOrder(1, new OrderCreationDto() { ShippingAddress = "gjh", OrderItems = new List<OrderItemDto>() { new OrderItemDto() { ProductID = 10, Quantity = 4 } } });
            // Assert
            var ex = Assert.ThrowsAsync<ProductNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Product with ID 10 not found."));
        }
        [Test]
        public async Task PlaceOrderProductQuantity()
        {
            // Arrange                              

            // Action
            var result = orderService.PlaceOrder(1, new OrderCreationDto() { ShippingAddress = "2213", OrderItems = new List<OrderItemDto>() { new OrderItemDto() { ProductID = 1, Quantity = 4 } } });
            // Assert
            var ex = Assert.ThrowsAsync<ProductNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Product with ID 1 not found."));
        }
        [Test]
        public async Task UpdateOrderStatusFailed()
        {
            // Arrange                              

            // Action
            var result = orderService.UpdateOrderStatus(10, "paid");
            // Assert
            var ex = Assert.ThrowsAsync<NoOrderFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"No Order found with Id : 10"));
        }
        [Test]
        public async Task UpdateOrderStatusSuccess()
        {
            // Arrange                              

            // Action
            var result = await orderService.UpdateOrderStatus(1, "paid");
            // Assert
            Assert.That(result.Status, Is.EqualTo("paid"));

        }
        [Test]
        public async Task CancelOrderNotFound()
        {
            // Arrange                              

            // Action
            var result = orderService.CancelOrder(10);
            // Assert
            var ex = Assert.ThrowsAsync<NoOrderFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"No Order found with Id : 10"));
        }
        [Test]
        public async Task CancelOrder()
        {
            // Arrange                              

            // Action
            var result = await orderService.CancelOrder(1);
            // Assert
            Assert.That(result.OrderID, Is.EqualTo(1));

        }
    }
}
