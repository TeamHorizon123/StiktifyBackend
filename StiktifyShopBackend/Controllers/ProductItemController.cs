using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/product-item")]
    [ApiController]
    public class ProductItemController : ODataController
    {
        private IProductItemProvider _provider;
        public ProductItemController(IProductItemProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductItem>> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductItem>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            var item = await _provider.GetOne(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateItem([FromBody] RequestCreateProductItem request)
        {
            var response = await _provider.AddProductItem(request);
            if (response.StatusCode != StatusCodes.Status201Created)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { id = response.Message });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string id, [FromBody] RequestUpdateProductItem request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateProductItem(request);
            if (response.StatusCode != StatusCodes.Status200OK)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { id = response.Message });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id)
        {
            var response = await _provider.DeleteProductItem(id);
            return StatusCode(response.StatusCode, new { message = response.Message });
        }
    }
}
