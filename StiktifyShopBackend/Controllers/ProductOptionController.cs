using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/product-option")]
    [ApiController]
    [Authorize]
    public class ProductOptionController : ODataController
    {
        private IProductOptionProvider _provider;

        public ProductOptionController(IProductOptionProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ResponseProductOption>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var option = await _provider.GetOne(id);
            return option?.Id == null ? NotFound() : Ok(option);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOption([FromBody] RequestCreateOption request)
        {
            var response = await _provider.CreateProductOption(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOption([FromRoute] string id, [FromBody] RequestUpdateOption request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateProductOption(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOption([FromRoute] string id)
        {
            var response = await _provider.DeleteProductOption(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete-many")]
        public async Task<IActionResult> DeleteManyOption([FromBody] ICollection<string> ids)
        {
            var response = await _provider.DeleteManyProductOption(ids);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
