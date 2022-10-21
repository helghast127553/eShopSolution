using eShopSolution.Application.System;
using eShopSolution.BackendApi.Controllers;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;

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

            //Act
            var okResult = await controller.GetUsers(request);

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
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
    }
}