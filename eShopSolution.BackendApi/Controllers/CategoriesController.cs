using eShopSolution.Application.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Categories.Manage;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoriesController:ControllerBase
    {
        private readonly IPublicCategoryService _categoryPublicService;
        private readonly IManageCategoryService _categoryManageService;

        public CategoriesController(
            IPublicCategoryService categoryPublicService, IManageCategoryService categoryManageService)
        {
            _categoryPublicService = categoryPublicService;
            _categoryManageService = categoryManageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _categoryPublicService.GetAll();
            return Ok(new { data });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryPublicService.GetById(id);
            return Ok(category);
        }

        [HttpGet("parentCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllParentCategoryPaging()
        {
            var data = await _categoryManageService.GetAllParentCategory();
            return Ok(new { data });
        }

        [HttpGet("subCategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllSubCategory()
        {
            var data = await _categoryManageService.GetAllSubCategory(); ;
            return Ok(new { data });
        }

        [HttpGet("subCategory/paging")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllSubCategoryPaging([FromQuery] GetCategoryManagePagingRequest request)
        {
            var data = await _categoryManageService.GetAllSubCategoryPaging(request);
            return Ok(new { data });
        }


        [HttpPost("category/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateRequest request)
        {
            var result = await _categoryManageService.Create(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("category/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateRequest request)
        {
            var result = await _categoryManageService.Update(id, request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("category/{id}/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryManageService.Delete(id);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
