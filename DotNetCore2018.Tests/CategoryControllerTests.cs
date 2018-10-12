using Moq;
using NUnit.Framework;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Tests.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DotNetCore2018.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using DotNetCore2018.WebApi.ViewModels;
using DotNetCore2018.Data.Entities;
using System.Linq;
using System.IO;
using System;

namespace DotNetCore2018.Tests
{
    public class CategoryControllerTests
    {
        private IConfiguration _configuration;
        private ILogger<CategoryController> _logger;

        [SetUp]
        public void Setup()
        {
            _configuration = TestHelper.BuildConfig();
            _logger = TestHelper.BuildLogger<CategoryController>();
        }

        [Test]
        public void Index_ReturnsEmptyList()
        {
            // Arrange
            var dataService = new InMemoryDataService();
            var controller = new CategoryController(dataService, _logger, _configuration);

            // Act
            var result = controller.Index();
            var model = result.ExtractModel<CategoryViewModel[]>();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Length, 0);
        }

        [Test]
        public void Create_ReturnOneElement()
        {
            // Arrange
            var dataService = new InMemoryDataService();
            var controller = new CategoryController(dataService, _logger, _configuration);

            // Act
            controller.Create(new CategoryViewModel() { Name = "test" });

            // Assert
            var data = dataService.GetAll<Category>().ToArray();
            Assert.AreEqual(data.Length, 1);
            Assert.AreEqual(data.First().Name, "test");
        }
    }
}