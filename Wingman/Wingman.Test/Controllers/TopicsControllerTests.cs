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

namespace Wingman.Test.Controllers
{
    [TestClass]
    public class TopicsControllerTests
    {
        //TODO: Not working correctly
        [TestMethod]
        public void GetTopicsShouldReturnTopics()
        {
            //Arrange
            WebApiConfig.CreateMaps();
            var _mockTopicRepository = new Mock<ITopicRepository>();
            _mockTopicRepository.Setup(m => m.GetAll())
                                 .Returns(new List<Topic>
                                      {
                                          new Topic { TopicId = 1,
                                                                  TopicName = "topic name"},
                                          new Topic { TopicId = 2,
                                                                  TopicName = "topic name2"}
                                      });
            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);

            // Act
            var httpResponse = controller.GetTopics();

            // Assert
            Assert.IsNotNull(httpResponse);

            OkNegotiatedContentResult<List<TopicModel>> okHttpResponse = (OkNegotiatedContentResult<List<TopicModel>>)httpResponse;
            Assert.IsNotNull(okHttpResponse);
            Assert.IsNotNull(okHttpResponse.Content);

            var domainResponse = okHttpResponse.Content;

            Assert.AreEqual(domainResponse.Count, 2);
        }

        //TODO: Not working correctly
        [TestMethod]
        public void GetTopicsShouldReturnNotFound()
        {
            // Arrange
            WebApiConfig.CreateMaps();
            var _mockTopicRepository = new Mock<ITopicRepository>();
            _mockTopicRepository.Setup(m => m.GetAll())
                                          .Returns(new List<Topic>
                                          { });


            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);

            // Act
            var httpResponse = controller.GetTopics();

            // Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsInstanceOfType(httpResponse, typeof(NotFoundResult));

        }
        [TestMethod]
        public void GetTopicShouldReturnTopic()
        {
            //Arrange
            WebApiConfig.CreateMaps();
            var _mockTopicRepository = new Mock<ITopicRepository>();
            _mockTopicRepository.Setup(m => m.GetById(1))
                                  .Returns(new Core.Domain.Topic
                                  {
                                      TopicId = 1,
                                      TopicName = "topic name"
                                  });
            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);

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
            WebApiConfig.CreateMaps();
            var _mockTopicRepository = new Mock<ITopicRepository>();
            _mockTopicRepository.Setup(m => m.GetById(1))
                                   .Returns(new Core.Domain.Topic
                                   {
                                       TopicId = 1,
                                       TopicName = "topic name"
                                   });


            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);

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
            WebApiConfig.CreateMaps();
            var _mockTopicRepository = new Mock<ITopicRepository>();
                                    _mockTopicRepository.Setup(m => m.GetById(1))
                                   .Returns(new Core.Domain.Topic
                                   {
                                       TopicId = 1,
                                       TopicName = "topic name"
                                   });


            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new TopicsController(_mockTopicRepository.Object, _mockUnitOfWork.Object);

            // Act
            TopicModel topic = new TopicModel();
            topic.TopicId = 1;
            topic.TopicName = "topic name changed";

            var httpResponse = controller.PutTopic(1, topic);
            
            // Assert
            Assert.IsNotNull(httpResponse);
            Assert.AreEqual(httpResponse, 200);
        }

    }
}
