using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : ODataController
    {
        private ICategoryProvider _provider;

        public CategoryController(ICategoryProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategory>> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategory>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/children")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategory>> GetAllChildren([FromRoute] string id)
        {
            var list = _provider.GetAllChildren(id).AsQueryable();
            return Ok(list);
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var category = await _provider.GetOne(id);
            return category?.Id == null ? NotFound() : Ok(category);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] RequestCreateCategory request)
        {
            var response = await _provider.CreateCategory(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] RequestUpdateCategory request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateCategory(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            var response = await _provider.DeleteCategory(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete-many")]
        public async Task<IActionResult> DeleteMany([FromBody] string[] ids)
        {
            var response = await _provider.DeleteMany(ids);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
