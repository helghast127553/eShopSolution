using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new ApiSuccessResult<string>(CreateToken(user, request.UserName, roles));
        }

        private string CreateToken(AppUser user, string username, IList<string> roles) 
        { 
            var signingCredentials = GetSigningCredentials(); 
            var claims = GetClaims(user, username, roles);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions); 
        }

        private Claim[] GetClaims(AppUser user, string username, IList<string> roles) 
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, username),
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IList<Claim> claims) 
        { 
            var tokenOptions = new JwtSecurityToken
                (issuer: _config["JwtSettings:validIssuer"], 
                audience: _config["JwtSettings:validIssuer"], 
                claims: claims, 
                expires: DateTime.Now.AddHours(int.Parse(_config["JwtSettings:expires"])), 
                signingCredentials: signingCredentials); 
            return tokenOptions; 
        }

        private SigningCredentials GetSigningCredentials() 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }

            user = new AppUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsers(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .Select(x => new UserViewModel
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToListAsync();

            var pagedResult = new PagedResult<UserViewModel> 
            { 
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize.Value,
                items = data
            };

            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }
    }
}
