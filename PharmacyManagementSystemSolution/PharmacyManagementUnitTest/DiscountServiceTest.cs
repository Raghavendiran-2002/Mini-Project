using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.DiscountDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public class DiscountServiceTest
    {
        DBPharmacyContext context;
        IRepository<int, Discount> discount;
        IDiscountService discountService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("dummyDsB");
            context = new DBPharmacyContext(optionsBuilder.Options);
            discount = new DiscountRepository(context);
            discountService = new DiscountService(discount);
            discount.Add(new Discount() { DiscountName = "Bank", DiscountPercentage = 10, StartDate = new DateTime(2024, 5, 15), EndDate = new DateTime(2024, 6, 8) });
        }

        [Test]
        public async Task AddDiscount()
        {
            // Arrange
            AddDiscountDTO dis = new AddDiscountDTO() { DiscountName = "Off", DiscountPercentage = 15, StartDate = new DateTime(2024, 5, 15), EndDate = new DateTime(2024, 6, 8) };
            // Action
            var result = await discountService.AddDiscount(dis);
            // Assert
            Assert.That(result.DiscountID , Is.EqualTo(2));
        }
        [Test]
        public async Task GetDiscountById()
        {
            // Arrange
           
            // Action
            var result = await discountService.GetDiscountById(1);
            // Assert
            Assert.That(result.DiscountID, Is.EqualTo(1));
        }
        [Test]
        public async Task GetDiscounts()
        {
            // Arrange
         
            // Action
            var result = await discountService.GetDiscounts();
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task UpdateDiscount()
        {
            // Arrange
            UpdateDiscountDTO discount = new UpdateDiscountDTO() { DiscountID = 1, DiscountName = "renamed", DiscountPercentage = 25, StartDate = DateTime.Now, EndDate = DateTime.Now };
            // Action
            var result = await discountService.UpdateDiscount(discount);
            // Assert
            Assert.That(result.DiscountName, Is.EqualTo("renamed"));
        }
        [Test]
        public async Task DeleteDiscount()
        {
            // Arrange
            AddDiscountDTO dis = new AddDiscountDTO() { DiscountName = "Off", DiscountPercentage = 15, StartDate = new DateTime(2024, 5, 15), EndDate = new DateTime(2024, 6, 8) };
            await discountService.AddDiscount(dis);
            // Action
            var result = await discountService.DeleteDiscount(2);
            // Assert
            Assert.That(result.DiscountID, Is.EqualTo(2));
        }
    }
}
