using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : ODataController
    {
        private ICategoryRepo _repo;
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCategory>> GetAll()
        {
            var listCategory = _repo.GetAll().AsQueryable();
            return Ok(listCategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var category = await _repo.GetById(id);
            return category == null ? NotFound() : Ok(new { value = category });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategory category)
        {
            var response = await _repo.Create(category);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCategory category)
        {
            if (id != category.Id)
                return BadRequest("Category ID mismatch.");
            var response = await _repo.Update(category);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
