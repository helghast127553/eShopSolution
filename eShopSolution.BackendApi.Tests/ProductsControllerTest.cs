using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.System;
using eShopSolution.BackendApi.Controllers;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eShopSolution.BackendApi.Tests
{
    public class ProductsControllerTest
    {
        [Fact]
        public async Task GetAllProductPaging_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var request = new GetManageProductPagingRequest()
            {
                PageIndex = 1,
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var okResult = await controller.GetAllProductPaging(request);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductCreateRequest { 
                Name = "Trong", 
                Description = "sdadsa", 
                Price = 65645, 
                CategoryId = 1 
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("Description", "Required");
            controller.ModelState.AddModelError("Price", "Required");
            controller.ModelState.AddModelError("CategoryId", "Required");

            // Act
            var badResponse = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Add_Success_ReturnsOkResult()
        {
            //Arrange
            var request = new ProductCreateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Create(request))
              .ReturnsAsync(1);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);
            
            // Act
            var OkResult = await controller.Create(request);

            // Assert
            Assert.IsType<OkResult>(OkResult);
        }

        [Fact]
        public async Task Add_Fail_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductCreateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Create(request))
              .ReturnsAsync(0);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var badResult = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestResult>(badResult);
        }

        [Fact]
        public async Task Add_ErrorWhileAddingNewItem_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductCreateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1,
                ThumbnailImage = new FormFile(new MemoryStream(), 0, new MemoryStream().Length, "dsadsa", "dsadsad")
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var badResponse = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Edit_Success_ReturnsOKResult()
        {
            //Arrange
            var request = new ProductUpdateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Update(1, request))
              .ReturnsAsync(1);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var okResult = await controller.Update(1, request);

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task Edit_Fail_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductUpdateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Update(1, request))
              .ReturnsAsync(0);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var badResponse = await controller.Update(1, request);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Edit_InvalidObjectPassed_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductUpdateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("Description", "Required");
            controller.ModelState.AddModelError("Price", "Required");
            controller.ModelState.AddModelError("CategoryId", "Required");

            // Act
            var badResponse = await controller.Update(1, request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Edit_ErrorWhileExecuteUpdatingData_ReturnsBadRequest()
        {
            //Arrange
            var request = new ProductUpdateRequest
            {
                Name = "Trong",
                Description = "sdadsa",
                Price = 65645,
                CategoryId = 1
            };

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var badResponse = await controller.Update(1, request);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Remove_ErrorWhileExecuteDeleteData_ReturnsBadRequest()
        {
            //Arrange
            int id = 5;

            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var badResponse = await controller.Delete(id);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Remove_Success_ReturnsNoContent()
        {
            //Arrange
            int id = 5;

            var publicProductService = new Mock<IPublicProductService>();

            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Delete(id))
              .ReturnsAsync(1);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Remove_Fail_ReturnsNotFound()
        {
            //Arrange
            int id = 5;

            var publicProductService = new Mock<IPublicProductService>();

            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Delete(id))
              .ReturnsAsync(-1);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Remove_Fail_ReturnsBadRequest()
        {
            //Arrange
            int id = 5;

            var publicProductService = new Mock<IPublicProductService>();

            var manageProductService = new Mock<IManageProductService>();
            manageProductService.Setup(x => x.Delete(id))
              .ReturnsAsync(0);

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
