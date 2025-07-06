using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/category-size")]
    [ApiController]
    public class CategorySizeController : ODataController
    {
        private ICategorySizeProvider _provider;
        public CategorySizeController(ICategorySizeProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategorySize>> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategorySize>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/category")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategorySize>> GetAllOfCategory([FromRoute] string id)
        {
            var list = _provider.GetAllOfCategory(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product-option")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategorySize>> GetAllOfProductOption([FromRoute] string id)
        {
            var list = _provider.GetAllOfProductOption(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var categorySize = await _provider.GetOne(id);
            return categorySize?.Id == null ? NotFound() : Ok(categorySize);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategorySize([FromBody] RequestCreateCategorySize request)
        {
            var response = await _provider.AddCategorySize(request);
            if (response.StatusCode != StatusCodes.Status201Created)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { id = response.Message });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategorySize([FromRoute] string id, [FromBody] RequestUpdateCategorySize request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateCategorySize(request);
            return StatusCode(response.StatusCode, new { message = response.Message });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategorySize([FromRoute] string id)
        {
            var response = await _provider.DeleteCategorySize(id);
            return StatusCode(response.StatusCode, new { message = response.Message });
        }
    }
}
