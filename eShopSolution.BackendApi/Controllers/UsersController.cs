using eShopSolution.Application.System;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("auth/token/")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Authenticate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("auth/register/")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(request);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("auth/user-info/")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUserInfo()
        {
            var data = new
            {
                username = User.FindFirstValue(ClaimTypes.Name),
                email = User.FindFirstValue(ClaimTypes.Email),
                roles = User.FindFirstValue(ClaimTypes.Role),
            };

            return Ok(new { data });
        }

        [HttpGet("auth/users/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers([FromQuery]GetUserPagingRequest request)
        {
            var data = await _userService.GetUsersPaging(request);
            return Ok(new { data });
        }
    }
}
