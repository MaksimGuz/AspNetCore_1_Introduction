using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using BaseSiteWebApp.Controllers;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.ViewModels;
using System;

namespace XUnitTestProject
{
    public class CategoriesControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCategories()
        {
            // Arrange
            var mockService = new Mock<ICategoriesService>();
            mockService.Setup(srv => srv.GetAllAsync()).ReturnsAsync(new CategoriesIndexViewModel
            {
                Categories = new List<Categories>() {
                    new Categories() { CategoryId = 1 },
                    new Categories() { CategoryId = 2 }
                }
            });
            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CategoriesIndexViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model.Categories);
            Assert.Equal(2, model.Categories.Count());
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAnEmptyListOfCategories()
        {
            // Arrange
            var mockService = new Mock<ICategoriesService>();
            mockService.Setup(srv => srv.GetAllAsync()).ReturnsAsync(new CategoriesIndexViewModel());
            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CategoriesIndexViewModel>(viewResult.ViewData.Model);
            Assert.Null(model.Categories);            
        }

        [Fact]
        public async Task Details_ThrowsException_WithNullId()
        {
            // Arrange
            var mockService = new Mock<ICategoriesService>();
            mockService.Setup(srv => srv.GetByIdAsync(1)).ReturnsAsync(new Categories() { CategoryId = 1 });
            var controller = new CategoriesController(mockService.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Details(null));
        }

        [Fact]
        public async Task Details_ReturnCategoryWithExistingId()
        {
            // Arrange
            var mockService = new Mock<ICategoriesService>();
            mockService.Setup(srv => srv.GetByIdAsync(1)).ReturnsAsync(new Categories() { CategoryId = 1 });
            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Categories>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(1, model.CategoryId);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            int id = 2;
            var mockService = new Mock<ICategoriesService>();
            mockService.Setup(srv => srv.GetByIdAsync(1)).ReturnsAsync(new Categories() { CategoryId = 1 });
            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Details(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}