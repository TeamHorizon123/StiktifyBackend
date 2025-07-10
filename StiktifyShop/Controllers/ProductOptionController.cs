using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/product-option")]
    [ApiController]
    //[Authorize]
    public class ProductOptionController : ODataController
    {
        private IProductOptionRepo _repo;
        public ProductOptionController(IProductOptionRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductOption>> GetAll()
        {
            var listProductOption = _repo.GetAll().AsQueryable();
            return Ok(listProductOption);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var productOption = await _repo.Get(id);
            return productOption == null ? NotFound() : Ok(new { value = productOption });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductOption option)
        {
            var response = await _repo.Create(option);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateProductOption option)
        {
            if (id != option.Id)
                return BadRequest("Product option ID mismatch.");
            var response = await _repo.Update(option);
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
