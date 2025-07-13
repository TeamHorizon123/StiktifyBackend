using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/product-variant")]
    [ApiController]
    //[Authorize]
    public class ProductVariantController : ODataController
    {
        private IProductVariantRepo _repo;
        public ProductVariantController(IProductVariantRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductVariant>> GetAll()
        {
            var listVariant = _repo.GetAll().AsQueryable();
            return Ok(listVariant);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var productVariant = await _repo.Get(id);
            return productVariant == null ? NotFound() : Ok(new { value = productVariant });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductVariant variant)
        {
            var response = await _repo.Create(variant);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateProductVariant variant)
        {
            if (id != variant.Id)
                return BadRequest("Product variant ID mismatch.");
            var response = await _repo.Update(variant);
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
