using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wingman.Core.Repository;
using Moq;
using Wingman.Core.Infrastructure;
using Wingman.Controllers;
using Wingman.Core.Models;
using System.Web.Http.Results;
using System.Collections.Generic;
using Wingman.Core.Domain;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Linq;

namespace Wingman.Test.Controllers
{
    [TestClass]
    public class TopicsControllerTests
    {
        Mock<ITopicRepository> _mockTopicRepository;
        Mock<IUnitOfWork> _mockUnitOfWork;
        Mock<IWingmanUserRepository> _mockUserRepository;

        TopicsController controller;

        [TestInitialize]
        public void Initialize()
        {
            WebApiConfig.CreateMaps();
            _mockTopicRepository = new Mock<ITopicRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUserRepository = new Mock<IWingmanUserRepository>();


            controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);
        }

        [TestMethod]
        public void GetTopicsShouldReturnTopics()
        {
            //Arrange
            _mockTopicRepository.Setup(m => m.GetAll())
                     .Returns(new List<Topic>
                          {
                                          new Topic { TopicId = 1,
                                                                  TopicName = "topic name"},
                                          new Topic { TopicId = 2,
                                                                  TopicName = "topic name2"}
                          });

            // Act
            var topics = controller.GetTopics();

            // Assert
            Assert.IsNotNull(topics);

            Assert.IsTrue(topics.Count() == 2);
        }

        [TestMethod]
        public void GetTopicsShouldReturnNotFound()
        {
            // Arrange
            _mockTopicRepository.Setup(m => m.GetAll())
                                          .Returns(new List<Topic>
                                          { });

            // Act
            var httpResponse = controller.GetTopics();

            // Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsTrue(httpResponse.Count() == 0);

        }

        [TestMethod]
        public void GetTopicShouldReturnTopic()
        {
            //Arrange
            _mockTopicRepository.Setup(m => m.GetById(1))
                                  .Returns(new Core.Domain.Topic
                                  {
                                      TopicId = 1,
                                      TopicName = "topic name"
                                  });

            // Act
            var httpResponse = controller.GetTopic(1);

            // Assert
            Assert.IsNotNull(httpResponse);

            OkNegotiatedContentResult<TopicModel> okHttpResponse = (OkNegotiatedContentResult<TopicModel>)httpResponse;
            Assert.IsNotNull(okHttpResponse);
            Assert.IsNotNull(okHttpResponse.Content);

            var domainResponse = okHttpResponse.Content;

            Assert.AreEqual(domainResponse.TopicId, 1);
        }

        [TestMethod]
        public void GetTopicShouldReturnNotFound()
        {
            // Arrange
            _mockTopicRepository.Setup(m => m.GetById(1))
                                   .Returns(new Core.Domain.Topic
                                   {
                                       TopicId = 1,
                                       TopicName = "topic name"
                                   });

            // Act
            var httpResponse = controller.GetTopic(2);

            // Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsInstanceOfType(httpResponse, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutTopicReturnStatusCode()
        {
            // Arrange
            _mockTopicRepository.Setup(m => m.GetById(10))
                                .Returns(new Topic
                                {
                                    TopicId = 10,
                                    TopicName = "Product"
                                });


            // Act
            IHttpActionResult actionResult = controller.PutTopic(10, new TopicModel { TopicId = 10, TopicName = "Product" });
            var contentResult = actionResult as StatusCodeResult;

            // Assert
            _mockTopicRepository.Verify(tr => tr.Update(It.IsAny<Topic>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);

            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.StatusCode == HttpStatusCode.NoContent);
        }

    }
}
