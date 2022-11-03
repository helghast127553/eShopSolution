using eShopSolution.Application.System;
using eShopSolution.BackendApi.Controllers;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Xunit;

namespace eShopSolution.BackendApi.Tests
{
    public class UsersControllerTest
    {
        [Fact]
        public async Task GetUsers_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var userServices = new Mock<IUserService>();
            var controller = new UsersController(userServices.Object);
            var request = new GetUserPagingRequest { PageIndex = 1 };

            userServices.Setup(x => x.GetUsersPaging(request)).ReturnsAsync(new ApiSuccessResult<PagedResult<UserViewModel>>(new PagedResult<UserViewModel>
            {
               PageIndex = 1,
               PageSize = 10,
               TotalRecords = 30,
               items = new List<UserViewModel> 
               {
                   new UserViewModel { Id = new Guid("1F89A0C2-F362-4E54-5E3D-08DAB8CCCB45"), FirstName= "Trong", LastName = "Nghia", UserName = "helghast127553", Email = "nghia.t6363@gmail.com" },
                   new UserViewModel { Id = new Guid("1F89A0C2-F362-4E54-5E3D-08DAB8CCCB45"), FirstName= "Toan", LastName = "Bach", UserName = "admin", Email = "tedu.international@gmail.com" }
               }
            }));

            //Act
            var result = await controller.GetUsers(request);
            var resultObj = result as OkObjectResult;
            var resultExpectation = resultObj.Value as ApiSuccessResult<PagedResult<UserViewModel>>;

            //Assert
            Assert.NotEmpty(resultExpectation.ResultObj.items);
            Assert.True(resultExpectation.IsSuccessed);
            Assert.Null(resultExpectation.Message);
            Assert.Equal(2, resultExpectation.ResultObj.items.Count());
            Assert.IsType<ApiSuccessResult<PagedResult<UserViewModel>>>(resultExpectation);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginAccount_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var request = new LoginRequest()
            {
                UserName = "helghast127553",
                Password = "12345678"
            };
            var userServices = new Mock<IUserService>();
            var controller = new UsersController(userServices.Object);

            controller.ModelState.AddModelError("UserName", "Required");
            controller.ModelState.AddModelError("Password", "Required");

            // Act
            var badResponse = await controller.Authenticate(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task LoginAccount_ReturnsToken()
        {
            // Arrange
            var request = new LoginRequest()
            {
                UserName = "helghast127553",
                Password = "12345678"
            };
            var userServices = new Mock<IUserService>();
            userServices.Setup(x => x.Authenticate(request))
                .ReturnsAsync(new ApiSuccessResult<string>("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"));
            var controller = new UsersController(userServices.Object);

            // Act
            var result = await controller.Authenticate(request);
            var resultObj = result as OkObjectResult;
            var tokenExpectation = resultObj.Value as ApiResult<string>;

            // Assert
            Assert.Null(tokenExpectation.Message);
            Assert.NotNull(tokenExpectation.ResultObj);
            Assert.IsType<string>(tokenExpectation.ResultObj);
            Assert.IsType<OkObjectResult>(resultObj);
            Assert.True(tokenExpectation.IsSuccessed);
            Assert.Equal("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", tokenExpectation.ResultObj);
        }

        [Fact]
        public async Task LoginAccount_EmptyToken()
        {
            // Arrange
            var request = new LoginRequest()
            {
                UserName = "helghast127553",
                Password = "12345678"
            };
            var userServices = new Mock<IUserService>();
            userServices.Setup(x => x.Authenticate(request))
                .ReturnsAsync(new ApiErrorResult<string>(""));
            var controller = new UsersController(userServices.Object);

            // Act
            var result = await controller.Authenticate(request);
            var resultObj = result as BadRequestObjectResult;
            var resultExpectation = resultObj.Value as ApiErrorResult<string>;

            // Assert
            Assert.Null(resultExpectation.ResultObj);
            Assert.False(resultExpectation.IsSuccessed);
            Assert.IsType<BadRequestObjectResult>(resultObj);
            Assert.IsType<ApiErrorResult<string>>(resultExpectation);
        }

        [Fact]
        public async Task RegisterAccount_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterRequest()
            {
                FirstName = "Trong",
                LastName = "Nghia",
                Email =  "nghia.t6363@gmail.com",
                PhoneNumber = "0776933780",
                UserName = "helghast127553",
                Password = "12345678"
            };
            var userServices = new Mock<IUserService>();
            var controller = new UsersController(userServices.Object);

            controller.ModelState.AddModelError("FirstName", "Required");
            controller.ModelState.AddModelError("LastName", "Required");
            controller.ModelState.AddModelError("Email", "Required");
            controller.ModelState.AddModelError("PhoneNumber", "Required");
            controller.ModelState.AddModelError("UserName", "Required");
            controller.ModelState.AddModelError("Password", "Required");

            // Act
            var badResponse = await controller.Register(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Register_Success_ReturnsOkResult()
        {
            // Arrange
            var request = new RegisterRequest()
            {
                UserName = "helghast127553",
                Password = "12345678",
                FirstName = "Trong",
                LastName = "Nghia",
                Email = "nghia.t6363@gmail.com",
                PhoneNumber = "12345678901",
                ConfirmPassword = "12345678"
            };
            var userServices = new Mock<IUserService>();
            userServices.Setup(x => x.Register(request))
                .ReturnsAsync(new ApiSuccessResult<bool>());
            var controller = new UsersController(userServices.Object);

            // Act
            var result = await controller.Register(request);
            var resultObj = result as OkObjectResult;
            var resultExpectation = resultObj.Value as ApiSuccessResult<bool>;

            // Assert
            Assert.NotNull(resultExpectation);
            Assert.Null(resultExpectation.Message);
            Assert.IsType<ApiSuccessResult<bool>>(resultExpectation);
            Assert.IsType<OkObjectResult>(resultObj);
            Assert.True(resultExpectation.IsSuccessed);
        }

        [Fact]
        public async Task Register_Failed_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterRequest()
            {
                UserName = "helghast127553",
                Password = "12345678",
                FirstName = "Trong",
                LastName = "Nghia",
                Email = "nghia.t6363@gmail.com",
                PhoneNumber = "12345678901",
                ConfirmPassword = "12345678"
            };
            var userServices = new Mock<IUserService>();
            userServices.Setup(x => x.Register(request))
                .ReturnsAsync(new ApiErrorResult<bool>(""));
            var controller = new UsersController(userServices.Object);

            // Act
            var result = await controller.Register(request);
            var resultObj = result as BadRequestObjectResult;
            var resultExpectation = resultObj.Value as ApiErrorResult<bool>;

            // Assert
            Assert.False(resultExpectation.ResultObj);
            Assert.False(resultExpectation.IsSuccessed);
            Assert.IsType<BadRequestObjectResult>(resultObj);
            Assert.IsType<ApiErrorResult<bool>>(resultExpectation);
            Assert.NotNull(resultExpectation);
        }
    }
}