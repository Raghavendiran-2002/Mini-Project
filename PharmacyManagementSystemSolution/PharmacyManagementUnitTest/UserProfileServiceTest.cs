﻿using EmployeeRequestTrackerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;
using PharmacyManagementSystem.Exceptions;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagementSystem.Exceptions.General;
using PharmacyManagementSystem.Exceptions.UserProfile;

namespace PharmacyManagementUnitTest
{
    public class UserProfileServiceTest
    {
        DBPharmacyContext context;
        IRepository<int, User> user;
        ITokenService token;
        IUserService userService;
       [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDsB");
            context = new DBPharmacyContext(optionsBuilder.Options);
            user = new UserRepository(context);
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            token = new TokenService(mockConfig.Object);
            userService = new UserService(user, token);
            userService.Register(new RegisterDTO()
            {
                Username = "Pranav",
                Password = "123",
                Email = "pranav@gmail.com",
                FullName = "Pranav",
                Address = "string",
                PhoneNumber = "435346346",
                Role = "Admin"
            });
            userService.Register(new RegisterDTO()
            {
                Username = "Pranav",
                Password = "123",
                Email = "pranav@gmail.com",
                FullName = "Pranav",
                Address = "string",
                PhoneNumber = "435346346",
                Role = "Admin"
            });
        }
        [Test]
        public async Task ResetPasswordSucessfull() {
            // Arrange                              
            
            IUserProfileService userProfileService = new UserProfileService(user);
            
            // Action
            var result = await userProfileService.ResetPassword(new ResetPasswordDTO() { PreviousPassword="123" ,NewPassword="1234",UserId =1});

            // Assert
            Assert.That(result.UserId, Is.EqualTo(1));           
        }
    
        [Test] 
        public async Task ResetPasswordFail()
        {
            // Arrange                 
            IUserProfileService userProfileService = new UserProfileService(user);
      
            // Action
            var results = userProfileService.ResetPassword(new ResetPasswordDTO() { PreviousPassword = "1263", NewPassword = "1234", UserId = 1 });

            // Assert
            var ex = Assert.ThrowsAsync<PasswordIncorrect>(() => results);
            Assert.That(ex.Message, Is.EqualTo("Invalid Password"));
        }
        [Test] 
        public async Task ResetPasswordWithWrongUserId()
        {
            // Arrange                   
            IUserProfileService userProfileService = new UserProfileService(user);     

            // Action
            var result = userProfileService.ResetPassword(new ResetPasswordDTO() { PreviousPassword = "1263", NewPassword = "1234", UserId = 8 });

            // Assert
            var ex = Assert.ThrowsAsync<ItemCannotBeNull>(() => result);
            Assert.That(ex.Message, Is.EqualTo("8 cannot be null"));
        }
        [Test]
        public async Task UpdateProfileSuccess()
        {
            // Arrange                   
            IUserProfileService userProfileService = new UserProfileService(user);

            // Action
            var result = await userProfileService.UpdateProfile(new UpdateUserDTO() {
                UserId = 1,
                Username = "Pranav",             
                Email = "dumm@gmail.com",
                FullName = "Pranav",
                Address = "string",
                PhoneNumber = "435346346",
                Role = "Admin"
            });

            // Assert
            Assert.That(result.Email, Is.EqualTo("dumm@gmail.com"));           
        }
        [Test]
        public async Task ViewProfileSuccess()
        {
            // Arrange                   
            IUserProfileService userProfileService = new UserProfileService(user);

            // Action
            var result = await userProfileService.ViewProfile(1);
        
            // Assert
            Assert.That(result.UserId, Is.EqualTo(1));
        }
        [Test]
        public async Task DeleteProfileSuccess()
        {
            // Arrange                   
            IUserProfileService userProfileService = new UserProfileService(user);

            // Action
            var result = await userProfileService.DeleteProfile(2);

            // Assert
            Assert.That(result.UserId, Is.EqualTo(2));
        }
    }
}