using eShopSolution.Application.System;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        //[HttpGet("paging")]
        //public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        //{
        //    var products = await _userService.GetUsersPaging(request);
        //    return Ok(products);
        //}

        [HttpPost("auth/token")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(new { access_token = result.ResultObj });
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("auth/user-info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(new { UserName = User.FindFirstValue(ClaimTypes.Name), 
                Email = User.FindFirstValue(ClaimTypes.Email), 
                Role = User.FindFirstValue(ClaimTypes.Role) });
        }
    }
}
