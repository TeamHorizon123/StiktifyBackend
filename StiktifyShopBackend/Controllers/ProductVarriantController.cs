using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/product-varriant")]
    [ApiController]
    public class ProductVarriantController : ControllerBase
    {
        private IProductVarriantProvider _provider;
        public ProductVarriantController(IProductVarriantProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponseProductVarriant>> GetAll()
        {
            var list = _provider.GetAll().AsEnumerable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        public ActionResult<IEnumerable<ResponseProductVarriant>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsEnumerable();
            return Ok(list);
        }

        [HttpGet("{id}/product-option")]
        public ActionResult<IEnumerable<ResponseProductVarriant>> GetAllOfProductOption([FromRoute] string id)
        {
            var list = _provider.GetAllOfProductOption(id).AsEnumerable();
            return Ok(list);
        }

        [HttpGet("get/{optionId}&{sizeId}")]
        public async Task<IActionResult> GetOne([FromRoute] string optionId, [FromRoute] string sizeId)
        {
            var varriant = await _provider.GetOne(optionId, sizeId);
            return varriant == null ? NotFound() : Ok(varriant);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVarriant([FromBody] RequestCreateProductVarriant request)
        {
            var response = await _provider.AddProductVarriant(request);
            if (response.StatusCode != StatusCodes.Status201Created)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { id = response.Message });
        }

        [HttpPut("update/{optionId}&{sizeId}")]
        public async Task<IActionResult> UpdateVarriant([FromRoute] string optionId, [FromRoute] string sizeId, [FromBody] RequestUpdateProductVarriant request)
        {
            if (optionId != request.ProductOptionId || sizeId != request.SizeId)
                return BadRequest("OptionId or SizeId does not match.");
            var response = await _provider.UpdateProductVarriant(request);
            if (response.StatusCode != StatusCodes.Status200OK)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { message = response.Message });
        }

        [HttpDelete("delete/{optionId}&{sizeId}")]
        public async Task<IActionResult> DeleteVarriant([FromRoute] string optionId, [FromRoute] string sizeId)
        {
            var response = await _provider.DeleteProductVarriant(optionId, sizeId);
            return StatusCode(response.StatusCode, new { message = response.Message });
        }
    }
}
