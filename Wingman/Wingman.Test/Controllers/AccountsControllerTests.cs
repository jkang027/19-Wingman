using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wingman.Controllers;
using Moq;
using Wingman.Core.Infrastructure;
using Wingman.Core.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;

namespace Wingman.Test.Controllers
{
    [TestClass]
    public class AccountsControllerTests
    {
        //[TestMethod]
        //public async Task RegisterShouldRegisterUser() // MethodShouldDoSomething
        //{
        //    // Arrange
        //    var _authRepository = new Mock<IAuthorizationRepository>();
        //    var controller = new AccountsController(_authRepository.Object);

        //    // Act
        //    var registration = new RegistrationModel
        //    {
        //        Username = "cameron",
        //        Password = "password1234",
        //        ConfirmPassword = "password1234",
        //        EmailAddress = "cameron@wilby.com",
        //        Gender = Core.Domain.Gender.Male
        //    };

        //    var response = await controller.Register(registration);

        //    // Assert
        //    Assert.IsInstanceOfType(response, typeof(OkResult));
        //}
    }
}
