using eShopSolution.Application.Catalog.Categories;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.BackendApi.Controllers;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Categories.Manage;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;

namespace eShopSolution.BackendApi.Tests
{
    public class CategoriesControllerTest
    {
        [Fact]
        public async Task GetAllCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            publicProductService.Setup(x => x.GetAll()).ReturnsAsync(new List<CategoryViewModel>
            {
                new CategoryViewModel { Id = 1, Name = "Laptop", Description = "Laptop", ParentId = null },
                new CategoryViewModel { Id = 1, Name = "Mobile Phone", Description = "Mobile Phone", ParentId = null },
                new CategoryViewModel { Id = 1, Name = "Tablet", Description = "Tablet", ParentId = null }
            });

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.GetAll();
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as List<CategoryViewModel>;

            // Assert
            Assert.NotEmpty(resultExpectation);
            Assert.NotNull(resultExpectation);
            Assert.Equal(3, resultExpectation.Count());
            Assert.IsType<List<CategoryViewModel>>(resultExpectation);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAllSubCategoryPaging_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var request = new GetCategoryManagePagingRequest()
            {
                PageIndex = 1,
            };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.GetAllSubCategoryPaging(request)).ReturnsAsync(new ApiSuccessResult<PagedResult<CategoryViewModel>>(
                new PagedResult<CategoryViewModel>
                {
                    PageIndex = 1,
                    PageSize = 15,
                    TotalRecords = 10,
                    items = new List<CategoryViewModel>
                    {
                        new CategoryViewModel {
                            Id = 1,
                            Name = "Asus",
                            Description = "Asus",
                            ParentId = 1,
                        },
                        new CategoryViewModel {
                            Id = 1,
                            Name = "Acer",
                            Description = "Acer",
                            ParentId = 1,
                        },
                        new CategoryViewModel {
                            Id = 1,
                            Name = "Dell",
                            Description = "Dell",
                            ParentId = 1,
                        }
                    }
                }));

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.GetAllSubCategoryPaging(request);
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as ApiSuccessResult<PagedResult<CategoryViewModel>>;

            // Assert
            Assert.Null(resultExpectation.Message);
            Assert.NotEmpty(resultExpectation.ResultObj.items);
            Assert.Equal(3, resultExpectation.ResultObj.items.Count());
            Assert.Equal(true, resultExpectation.IsSuccessed);
            Assert.IsType<ApiSuccessResult<PagedResult<CategoryViewModel>>>(resultExpectation);
            Assert.IsType<List<CategoryViewModel>>(resultExpectation.ResultObj.items);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAllSubCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.GetAllSubCategory()).ReturnsAsync(new List<CategoryViewModel>
            {
                new CategoryViewModel {
                            Id = 1,
                            Name = "Asus",
                            Description = "Asus",
                            ParentId = 1
                },
                new CategoryViewModel {
                            Id = 2,
                            Name = "Acer",
                            Description = "Acer",
                            ParentId = 1,
                },
                new CategoryViewModel {
                            Id = 3,
                            Name = "Dell",
                            Description = "Dell",
                            ParentId = 1
                },
            });

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.GetAllSubCategory();
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as List<CategoryViewModel>;

            // Assert
            Assert.NotEmpty(resultExpectation);
            Assert.Equal(3, resultExpectation.Count());
            Assert.IsType<List<CategoryViewModel>>(resultExpectation);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAllParentCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.GetAllParentCategory()).ReturnsAsync(new List<CategoryViewModel>
            {
                 new CategoryViewModel {
                            Id = 1,
                            Name = "Laptop",
                            Description = "Laptop",
                            ParentId = null
                },
                new CategoryViewModel {
                            Id = 2,
                            Name = "Mobile Phone",
                            Description = "Mobile Phone",
                            ParentId = null,
                },
                new CategoryViewModel {
                            Id = 3,
                            Name = "Tablet",
                            Description = "Tablet",
                            ParentId = null
                },
            });

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.GetAllParentCategory();
            var result = okResult as OkObjectResult;
            var resultExpectation = result.Value as List<CategoryViewModel>;

            // Assert
            Assert.NotEmpty(resultExpectation);
            Assert.Equal(3, resultExpectation.Count());
            Assert.IsType<List<CategoryViewModel>>(resultExpectation);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var request = new CategoryCreateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            controller.ModelState.AddModelError("Laptop", "Required");
            controller.ModelState.AddModelError("ParentId", "Required");

            // Act
            var badResponse = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Add_Success_ReturnsOKResult()
        {
            // Arrange
            var request = new CategoryCreateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Create(request)).ReturnsAsync(1);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.Create(request);

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task Add_Fail_ReturnsBadRequest()
        {
            // Arrange
            var request = new CategoryCreateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Create(request)).ReturnsAsync(0);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var badResponse = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Edit_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var request = new CategoryUpdateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            controller.ModelState.AddModelError("Laptop", "Required");
            controller.ModelState.AddModelError("ParentId", "Required");

            // Act
            var badResponse = await controller.Update(1, request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Edit_Success_ReturnsOKResult()
        {
            // Arrange
            var request = new CategoryUpdateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Update(1, request)).ReturnsAsync(1);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.Update(1, request);

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task Edit_Fail_ReturnsNotFound()
        {
            // Arrange
            var request = new CategoryUpdateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Update(1, request)).ReturnsAsync(-1);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var notFound = await controller.Update(1, request);

            // Assert
            Assert.IsType<NotFoundResult>(notFound);
        }


        [Fact]
        public async Task Edit_Fail_ReturnsBadRequest()
        {
            // Arrange
            var request = new CategoryUpdateRequest { Name = "Laptop", Description = "Laptop", ParentId = 1 };

            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Update(1, request)).ReturnsAsync(0);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var badResponse = await controller.Update(1, request);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Remove_Success_ReturnsOkResult()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Delete(1)).ReturnsAsync(1);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var okResult = await controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task Remove_Fail_ReturnsBadRequest()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Delete(1)).ReturnsAsync(0);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var badResponse = await controller.Delete(1);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Remove_Fail_ReturnsNotFound()
        {
            // Arrange
            var manageCategoryService = new Mock<IManageCategoryService>();
            var publicProductService = new Mock<IPublicCategoryService>();

            manageCategoryService.Setup(x => x.Delete(1)).ReturnsAsync(-1);

            var controller = new CategoriesController(publicProductService.Object, manageCategoryService.Object);

            // Act
            var notFound = await controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(notFound);
        }
    }
}
