using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BaseSiteWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XUnitTestProject
{
    public class ProductsControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetAllAsync()).ReturnsAsync(new List<Products>
            {
                new Products() { ProductId = 1 },
                new Products() { ProductId = 2 }
            });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAnEmptyListOfProducts()
        {
            // Arrange
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetAllAsync()).ReturnsAsync(new List<Products>());
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNullId()
        {
            // Arrange
            var mockService = new Mock<IProductsService>();            
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNonexistingId()
        {
            // Arrange
            int nonexistingId = 2;
            int productId = 1;
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(productId)).ReturnsAsync(new Products() { ProductId = productId });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Details(nonexistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnProductWithExistingId()
        {
            // Arrange
            int productId = 1;
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(1)).ReturnsAsync(new Products() { ProductId = productId });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Details(productId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Products>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(productId, model.ProductId);
        }

        [Fact]
        public async Task Create_ReturnViewResult()
        {
            //Arrange
            var mockProductsService = new Mock<IProductsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(srv => srv.GetCategoriesSelectListAsync(null)).ReturnsAsync(new SelectList(new List<Categories>()));
            var mockSuppliersService = new Mock<ISuppliersService>();
            mockSuppliersService.Setup(srv => srv.GetSuppliersSelectListAsync(null)).ReturnsAsync(new SelectList(new List<Suppliers>()));
            var controller = new ProductsController(mockProductsService.Object, mockCategoriesService.Object, mockSuppliersService.Object);

            // Act
            var result = await controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["CategoryId"]);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["SupplierId"]);
        }

        [Fact]
        public async Task Create_ReturnViewResult_GivenInvalidModel()
        {
            //Arrange
            int productId = 1;
            int categoriesId = 2;
            int suppliersId = 3;
            var mockProductsService = new Mock<IProductsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(srv => srv.GetCategoriesSelectListAsync(categoriesId))
                .ReturnsAsync(new SelectList(new List<Categories>{ new Categories { CategoryId = categoriesId } }));
            var mockSuppliersService = new Mock<ISuppliersService>();
            var controller = new ProductsController(mockProductsService.Object, mockCategoriesService.Object, mockSuppliersService.Object);
            mockSuppliersService.Setup(srv => srv.GetSuppliersSelectListAsync(suppliersId))
                .ReturnsAsync(new SelectList(new List<Suppliers> { new Suppliers { SupplierId = suppliersId } }));
            controller.ModelState.AddModelError("error", "some error");
            var products = new Products
            {
                ProductId = productId,
                SupplierId = suppliersId,
                CategoryId = categoriesId
            };

            // Act
            var result = await controller.Create(products);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["CategoryId"]);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["SupplierId"]);
        }

        [Fact]
        public async Task Create_ReturnsARedirectToIndexWhenProductHasBeenCreated()
        {
            //Arrange
            var products = new Products { ProductId = 1 };
            var mockProductsService = new Mock<IProductsService>();
            mockProductsService.Setup(srv => srv.Create(products)).Returns(Task.CompletedTask);
            var controller = new ProductsController(mockProductsService.Object, null, null);

            // Act
            var result = await controller.Create(products);

            //Assert
            var redirectToActionResult =
                    Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNullId()
        {
            // Arrange            
            var mockService = new Mock<IProductsService>();
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_ForNonexistingId()
        {
            // Arrange
            int nonexistingId = 2;
            int productId = 1;
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(productId)).ReturnsAsync(new Products() { ProductId = productId });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Edit(nonexistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult()
        {
            //Arrange
            int productsId = 1;
            int categoriesId = 2;
            int suppliersId = 3;
            var mockProductsService = new Mock<IProductsService>();
            mockProductsService.Setup(srv => srv.GetByIdAsync(productsId))
                .ReturnsAsync(new Products
                {
                    ProductId = productsId,
                    CategoryId = categoriesId,
                    SupplierId = suppliersId
                });
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(srv => srv.GetCategoriesSelectListAsync(categoriesId))
                .ReturnsAsync(new SelectList(new List<Categories> { new Categories { CategoryId = categoriesId } }));
            var mockSuppliersService = new Mock<ISuppliersService>();
            mockSuppliersService.Setup(srv => srv.GetSuppliersSelectListAsync(suppliersId))
                .ReturnsAsync(new SelectList(new List<Suppliers> { new Suppliers { SupplierId = suppliersId } }));
            var controller = new ProductsController(mockProductsService.Object, mockCategoriesService.Object, mockSuppliersService.Object);

            // Act
            var result = await controller.Edit(productsId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["CategoryId"]);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["SupplierId"]);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFound_IfIdDoesntMatch()
        {
            // Arrange
            int productId = 1;
            var product = new Products { ProductId = 2 };
            var mockService = new Mock<IProductsService>();
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Edit(productId, product);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsARedirectToIndexWhenProductHasBeenEdited()
        {
            //Arrange
            int productId = 1;
            var products = new Products { ProductId = productId };
            var mockProductsService = new Mock<IProductsService>();
            mockProductsService.Setup(srv => srv.Update(products)).Returns(Task.CompletedTask);
            var controller = new ProductsController(mockProductsService.Object, null, null);

            // Act
            var result = await controller.Edit(productId, products);

            //Assert
            var redirectToActionResult =
                    Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_GivenInvalidModel()
        {
            //Arrange
            int productId = 1;
            int categoriesId = 2;
            int suppliersId = 3;
            var mockProductsService = new Mock<IProductsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
            mockCategoriesService.Setup(srv => srv.GetCategoriesSelectListAsync(categoriesId))
                .ReturnsAsync(new SelectList(new List<Categories> { new Categories { CategoryId = categoriesId } }));
            var mockSuppliersService = new Mock<ISuppliersService>();
            var controller = new ProductsController(mockProductsService.Object, mockCategoriesService.Object, mockSuppliersService.Object);
            mockSuppliersService.Setup(srv => srv.GetSuppliersSelectListAsync(suppliersId))
                .ReturnsAsync(new SelectList(new List<Suppliers> { new Suppliers { SupplierId = suppliersId } }));
            controller.ModelState.AddModelError("error", "some error");
            var products = new Products
            {
                ProductId = productId,
                SupplierId = suppliersId,
                CategoryId = categoriesId
            };

            // Act
            var result = await controller.Edit(productId, products);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["CategoryId"]);
            Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["SupplierId"]);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNullId()
        {
            // Arrange            
            var mockService = new Mock<IProductsService>();
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsHttpNotFound_ForNonexistingId()
        {
            // Arrange
            int nonexistingId = 2;
            int productId = 1;
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(productId)).ReturnsAsync(new Products() { ProductId = productId });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Delete(nonexistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult()
        {
            //Arrange
            int productId = 1;
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(productId)).ReturnsAsync(new Products() { ProductId = productId });
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.Delete(productId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToIndex()
        {
            //Arrange
            int productId = 1;
            var products = new Products { ProductId = productId };
            var mockService = new Mock<IProductsService>();
            mockService.Setup(srv => srv.GetByIdAsync(productId)).ReturnsAsync(new Products() { ProductId = productId });
            mockService.Setup(srv => srv.Delete(products)).Returns(Task.CompletedTask);
            var controller = new ProductsController(mockService.Object, null, null);

            // Act
            var result = await controller.DeleteConfirmed(productId);

            //Assert
            var redirectToActionResult =
                                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
