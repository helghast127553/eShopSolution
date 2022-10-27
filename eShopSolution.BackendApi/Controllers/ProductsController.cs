using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Catalog.Products.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("product")]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        [HttpGet("product/detail/{id}/")]
        public async Task<IActionResult> GetProductDetailById([FromRoute] int id)
        {
            var productDetail = await _publicProductService.GetProductDetailById(id);
            return Ok(productDetail);
        }
            
        [HttpGet("product/paging/")]
        public async Task<IActionResult> GetAllByCategoryId([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryIdPaging(request);
            return Ok(products);
        }

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
        public async Task<IActionResult> GetAllProductPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var data = await _manageProductService.GetAllProductPaging(request); ;
            return Ok(new { data });
        }

        [HttpPost("product")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manageProductService.Create(request);

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("product/{id}/")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _manageProductService.Update(id, request);

            if (result == -1)
            {
                return NotFound();
            }

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
            var result = await _manageProductService.Delete(id);

            if (result == -1)
            {
                return NotFound();
            }

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
