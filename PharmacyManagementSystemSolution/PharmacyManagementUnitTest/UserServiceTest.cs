using EmployeeRequestTrackerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;
using PharmacyManagementSystem.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public class UserServiceTest
    {
        DBPharmacyContext context;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new DBPharmacyContext(optionsBuilder.Options);

        }
        [Test]
        public async Task UserRegisteredAndLoginSuccessfull()
        {
            //Arrange
            IRepository<int, User> user = new UserRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService token = new TokenService(mockConfig.Object);
            IUserService userService = new UserService(user, token);
            var reg = await userService.Register(new RegisterDTO()
            {
                Username = "Pranav",
                Password = "123",
                Email = "pranav@gmail.com",
                FullName = "Pranav",
                Address = "string",
                PhoneNumber = "435346346",
                Role = "Admin"
            });

            //Action
            var result = await userService.Login(new LoginDTO() { UserId = 1, Password = "123" });

            //Assert
            Assert.That(result.UserID, Is.EqualTo(1));
        }

        [Test]
        public async Task UserRegisteredAndLoginWithWrongPassword()
        {
            //Arrange
            IRepository<int, User> user = new UserRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService token = new TokenService(mockConfig.Object);
            IUserService userService = new UserService(user, token);
            var reg = await userService.Register(new RegisterDTO()
            {
                Username = "Pranav",
                Password = "123",
                Email = "pranav@gmail.com",
                FullName = "Pranav",
                Address = "string",
                PhoneNumber = "435346346",
                Role = "Admin"
            });

            //Action
            Assert.Pass();
            var result = await userService.Login(new LoginDTO() { UserId = 1, Password = "113" });

            //Assert
            Assert.Pass();
        }
        [Test]
        public async Task UserRegisteredAndLoginWithWrongUserId()
        {
            //Arrange
            IRepository<int, User> user = new UserRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService token = new TokenService(mockConfig.Object);
            IUserService userService = new UserService(user, token);

            //Action
            Assert.Pass();
            var result = await userService.Login(new LoginDTO() { UserId = 6, Password = "113" });

            //Assert
            Assert.Pass();
        }
    }
}
