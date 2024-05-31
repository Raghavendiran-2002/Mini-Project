using EmployeeRequestTrackerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.General;
using PharmacyManagementSystem.Exceptions.UserProfile;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
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
        IUserRepository<int, User> user;
        IUserRepository<int, User> userProfileRepo;
        IShoppingCartRepository<int, ShoppingCart> cartRepo;
        ITokenService token;
        IUserService userService;
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
            user = new UserRepository(context);
            userProfileRepo = new UserProfileRepositoy(context);
            cartRepo = new ShoppingCartRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            token = new TokenService(mockConfig.Object);
            userService = new UserService(user,cartRepo, token);
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
            user = new UserRepository(context);
            cartRepo = new ShoppingCartRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            token = new TokenService(mockConfig.Object);
            userService = new UserService(user, cartRepo, token);
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
            var result =  userService.Login(new LoginDTO() { UserId = 1, Password = "113" });

            //Assert
            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() => result);
            Assert.That(ex.Message, Is.EqualTo("Invalid username and password"));

        }
        [Test]
        public async Task UserRegisteredAndLoginWithWrongUserId()
        {
            //Arrange
            user = new UserRepository(context);
            cartRepo = new ShoppingCartRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            token = new TokenService(mockConfig.Object);
            userService = new UserService(user,cartRepo,  token);

            //Action
            
            var result =  userService.Login(new LoginDTO() { UserId = 6, Password = "113" });

            //Assert
            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() => result);
            Assert.That(ex.Message, Is.EqualTo("Invalid username and password"));
           
        }
    }
}
