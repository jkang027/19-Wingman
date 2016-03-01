using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wingman.Controllers;
using Wingman.Core.Repository;
using Moq;
using Wingman.Core.Infrastructure;
using System.Web.Http.Results;
using Wingman.Core.Models;

namespace Wingman.Test.Controllers
{
    [TestClass]
    public class ResponsesControllerTests
    {
        [TestMethod]
        public void GetResponseShouldReturnResponse()
        {
            // Arrange
            WebApiConfig.CreateMaps();
            var _mockResponseRepository = new Mock<IResponseRepository>();
            _mockResponseRepository.Setup(m => m.GetById(1))
                                   .Returns(new Core.Domain.Response
                                   {
                                       ResponseId = 1,
                                       ResponseText = "yadda yadda"
                                   });
            var _mockUserRepository = new Mock<IWingmanUserRepository>();


            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new ResponsesController(_mockResponseRepository.Object, _mockUnitOfWork.Object, _mockUserRepository.Object);

            // Act
            var httpResponse = controller.GetResponse(1);
           
            // Assert
            Assert.IsNotNull(httpResponse);

            OkNegotiatedContentResult<ResponseModel> okHttpResponse = (OkNegotiatedContentResult<ResponseModel>)httpResponse;
            Assert.IsNotNull(okHttpResponse);
            Assert.IsNotNull(okHttpResponse.Content);

            var domainResponse = okHttpResponse.Content;

            Assert.AreEqual(domainResponse.ResponseId, 1);

        }

        [TestMethod]
        public void GetResponseShouldReturnNotFound()
        {
            // Arrange
            WebApiConfig.CreateMaps();
            var _mockResponseRepository = new Mock<IResponseRepository>();
            _mockResponseRepository.Setup(m => m.GetById(1))
                                   .Returns(new Core.Domain.Response
                                   {
                                       ResponseId = 1,
                                       ResponseText = "yadda yadda"
                                   });


            var _mockUserRepository = new Mock<IWingmanUserRepository>();



            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new ResponsesController(_mockResponseRepository.Object, _mockUnitOfWork.Object, _mockUserRepository.Object);

            // Act
            var httpResponse = controller.GetResponse(2);

            // Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsInstanceOfType(httpResponse, typeof(NotFoundResult));
        }
    }
}
