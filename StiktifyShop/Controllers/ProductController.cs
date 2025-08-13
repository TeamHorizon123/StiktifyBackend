using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ODataController
    {
        private IProductRepo _repo;
        public ProductController(IProductRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProduct>> GetAll()
        {
            var listProduct = _repo.GetAll().AsQueryable();
            return Ok(listProduct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var product = await _repo.Get(id);
            return product == null ? NotFound() : Ok(new { value = product });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProduct product)
        {
            var response = await _repo.Create(product);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateProduct product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch.");
            var response = await _repo.Update(product);
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
