using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.Payments;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.PaymentDTOs;
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
    public class PaymentServiceTest
    {
        DBPharmacyContext context;
        IPaymentRepository<int, Payment> payment;
        IPaymentService paymentService;        
        IOrderRepository<int, Order> order;
        IOrderService orderService;
        IProductRepository<int, Product> product;
        IProductService productService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("DBReviewTest");
            context = new DBPharmacyContext(optionsBuilder.Options);
            order = new OrderRepository(context);            
            order.Add(new Order() { OrderID = 5, ShippingAddress = " ", UserID = 1, Status = "xcb", TotalAmount = 577 });            
            orderService = new OrderService(order, product);
            product = new ProductRepository(context);
            productService = new ProductService(product);
            payment = new PaymentRepository(context);
            paymentService = new PaymentService(payment, order);
        }
        [Test]
        public async Task CreatePayment()
        {
            // Arrange

            // Action
            var result = await paymentService.CreatePayment(1, new PaymentCreationDto() {OrderID = 5, Amount= 44,PaymentMethod = "card" });
            // Assert
            Assert.That(result.PaymentID, Is.EqualTo(1));

        }
        [Test]
        public async Task CreatePaymentOrderNotFound()
        {
            // Arrange

            // Action
            var result =  paymentService.CreatePayment(1, new PaymentCreationDto() { OrderID = 50, Amount = 44, PaymentMethod = "card" });
            // Assert
            var ex = Assert.ThrowsAsync<OrderNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Order not found or unauthorized access."));
        }
        [Test]
        public async Task CreatePaymentAmountPaid()
        {
            // Arrange

            // Action
            var result = await paymentService.CreatePayment(1, new PaymentCreationDto() { OrderID = 5, Amount = 4400, PaymentMethod = "card" });
            // Assert
            Assert.That(result.PaymentID, Is.EqualTo(2));

        }
        [Test]
        public async Task GetAllPayment()
        {
            // Arrange

            // Action
            var result = await paymentService.GetAllPayments();
            // Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task GetAllPGetPaymentsByOrderayment()
        {
            // Arrange

            // Action
            var result = paymentService.GetPaymentsByOrder(2, 1);
            // Assert
            var ex = Assert.ThrowsAsync<OrderNotFound>(() => result);
            Assert.That(ex.Message, Is.EqualTo($"Order not found or unauthorized access."));
        }
        [Test]
        public async Task GetPaymentsByUser()
        {
            // Arrange

            // Action
            var result = await paymentService.GetPaymentsByUser(2);
            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
