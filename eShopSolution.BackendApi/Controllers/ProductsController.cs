using eShopSolution.Application.Catalog.Products;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Catalog.Products.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        
        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        //http://localhost:port/product
        [HttpGet("product/")]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("public-paging/")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }

        //http://localhost:port/product/:id
        [HttpGet("product/{id}/")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _manageProductService.GetById(productId);
            if (product == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(product);
        }


        [HttpGet("manage/product/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get([FromQuery] GetManageProductPagingRequest request)
        {
            var data = await _manageProductService.GetAllPaging(request); ;
            return Ok(new { data });
        }

        [HttpPost("product/")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            var result = await _manageProductService.Create(request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("product/{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ProductUpdateRequest request)
        {
            var result = await _manageProductService.Update(id, request);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("product/{id}/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var productId = await _manageProductService.Delete(id);
            if (productId == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
