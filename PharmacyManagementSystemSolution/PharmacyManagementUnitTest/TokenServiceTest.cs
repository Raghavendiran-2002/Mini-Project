using Castle.Core.Configuration;
using EmployeeRequestTrackerAPI.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using PharmacyManagementSystem.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagementSystem.Models.DBModels;

namespace PharmacyManagementUnitTest
{
    public class TokenServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateTokenPassTest()
        {
            //Arrange
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<Microsoft.Extensions.Configuration.IConfiguration> mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService service = new TokenService(mockConfig.Object);

            //Action
            var token = service.GenerateToken(new User { UserID = 103, Role = "Admin" });

            //Assert
            Assert.IsNotNull(token);
        }
    }
}
