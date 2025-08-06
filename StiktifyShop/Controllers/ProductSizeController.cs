using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/product-size")]
    [ApiController]
    [Authorize]
    public class ProductSizeController : ODataController
    {
        private IProductSizeRepo _repo;
        public ProductSizeController(IProductSizeRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductSize>> GetAll()
        {
            var listSize = _repo.GetAll().AsQueryable();
            return Ok(listSize);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var size = await _repo.Get(id);
            return size == null ? NotFound() : Ok(new { value = size });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductSize productSize)
        {
            var response = await _repo.Create(productSize);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string id, [FromBody] UpdateProductSize productSize)
        {
            if (id != productSize.Id)
                return BadRequest("Product size ID mismatch.");
            var response = await _repo.Update(productSize);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
