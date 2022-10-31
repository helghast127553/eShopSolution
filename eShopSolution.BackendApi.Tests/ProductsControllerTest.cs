using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.System;
using eShopSolution.BackendApi.Controllers;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            manageProductService.Setup(x => x.GetAllProductPaging(request)).ReturnsAsync(new ApiSuccessResult<PagedResult<ProductViewModel>>(
                new PagedResult<ProductViewModel>
                {
                    PageIndex = 1,
                    PageSize = 15,
                    TotalRecords = 6,
                    items = new List<ProductViewModel>
                    {
                           new ProductViewModel { Id = 1, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                           new ProductViewModel { Id = 2, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                           new ProductViewModel { Id = 3, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                           new ProductViewModel { Id = 4, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                           new ProductViewModel { Id = 5, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                           new ProductViewModel { Id = 6, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                    }
                }));

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);


            // Act
            var okResult = await controller.GetAllProductPaging(request);
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as ApiSuccessResult<PagedResult<ProductViewModel>>;

            // Assert
            Assert.NotNull(resultExpectation.Message);
            Assert.NotEmpty(resultExpectation.ResultObj.items);
            Assert.Equal(6, resultExpectation.ResultObj.items.Count());
            Assert.Equal(true, resultExpectation.IsSuccessed);
            Assert.IsType<ApiSuccessResult<PagedResult<ProductViewModel>>>(resultExpectation);
            Assert.IsType<List<ProductViewModel>>(resultExpectation.ResultObj.items);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetroductDetail_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();

            publicProductService.Setup(x => x.GetProductDetailById(1)).ReturnsAsync(new ProductViewModel { 
                Id = 1, 
                Name = "iphone", 
                Description = "dsadasd",
                Price = 345,
                ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg"
            });

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var okResult = await controller.GetProductDetailById(1);
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as ProductViewModel;

            // Assert
            Assert.Equal(1, resultExpectation.Id);
            Assert.NotNull(resultExpectation);
            Assert.IsType<ProductViewModel>(resultExpectation);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAllByCategoryId_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var publicProductService = new Mock<IPublicProductService>();
            var manageProductService = new Mock<IManageProductService>();
            var request = new GetPublicProductPagingRequest {  SubCategoryId = 5, PageIndex = 1, PageSize = 15};

            publicProductService.Setup(x => x.GetAllByCategoryIdPaging(request)).ReturnsAsync(new PagedResult<ProductViewModel>
            {
                PageIndex = 1,
                PageSize = 15,
                TotalRecords = 35,
                items = new List<ProductViewModel>
                {
                    new ProductViewModel { Id = 1, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                    new ProductViewModel { Id = 1, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                    new ProductViewModel { Id = 1, ImageUrl = "https://localhost:7064/image/01ac7093-8ae2-47c6-bd53-9ce3ab7a3b89.jpg", Name = "Asus TUF Gaming", Description = "dsadsa", Price = 340 },
                }
            });

            var controller = new ProductsController(publicProductService.Object, manageProductService.Object);

            // Act
            var okResult = await controller.GetAllByCategoryId(request);
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as PagedResult<ProductViewModel>;

            // Assert
            Assert.Equal(3, resultExpectation.items.Count());
            Assert.NotNull(resultExpectation);
            Assert.NotEmpty(resultExpectation.items);
            Assert.IsType<ProductViewModel>(resultExpectation.items[0]);
            Assert.IsType<PagedResult<ProductViewModel>>(resultExpectation);
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
