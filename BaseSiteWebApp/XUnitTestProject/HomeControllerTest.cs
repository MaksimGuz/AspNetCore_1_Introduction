using Microsoft.AspNetCore.Identity.UI.Services;
using BaseSiteWebApp.Controllers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            //Arrange
            var controller = new HomeController(null, null);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            //Arrange
            var controller = new HomeController(null, null);

            //Act
            var result = controller.Privacy();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsViewResult()
        {
            //Arrange
            var mockLogger = Mock.Of<ILogger<HomeController>>();            
            var controller = new HomeController(null, mockLogger);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature() { Error = new Exception() });

            //Act
            var result = controller.Error();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}