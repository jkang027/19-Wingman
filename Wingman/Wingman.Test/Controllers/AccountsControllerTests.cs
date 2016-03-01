using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wingman.Controllers;
using Moq;
using Wingman.Core.Infrastructure;
using Wingman.Core.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Wingman.Core.Repository;
using Microsoft.AspNet.Identity;

namespace Wingman.Test.Controllers
{
    [TestClass]
    public class AccountsControllerTests
    {
        [TestMethod]
        public async Task RegisterShouldRegisterUser() // MethodShouldDoSomething
        {
            // Arrange
            var _authRepository = new Mock<IAuthorizationRepository>();
            _authRepository.Setup(a => a.RegisterUser(It.IsAny<RegistrationModel>()))
                           .Returns(Task.FromResult(IdentityResult.Success));

            var _mockUserRepository = new Mock<IWingmanUserRepository>();

            var controller = new AccountsController(_authRepository.Object, _mockUserRepository.Object);

            // Act
            var registration = new RegistrationModel
            {
                Username = "cameron",
                Password = "Lidk38cmmm",
                ConfirmPassword = "Lidk38cmmm",
                EmailAddress = "cameron@wilby.com",
                Gender = Core.Domain.Gender.Male
            };

            var response = await controller.Register(registration);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}
