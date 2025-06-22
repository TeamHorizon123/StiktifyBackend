using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/cart")]
    [ApiController]
    public class CartController : ODataController
    {
        private ICartProvider _provider;

        public CartController(ICartProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCart>> GetAllOfUser([FromRoute] string id)
        {
            var list = _provider.GetAllOfUser(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var cart = await _provider.GetOne(id);
            return cart?.Id == null ? NotFound() : Ok(cart);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCart([FromBody] RequestCreateCart request)
        {
            var response = await _provider.CreateCart(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCart([FromRoute] string id, [FromBody] RequestUpdateCart request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateCart(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] string id)
        {
            var response = await _provider.DeleteCart(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteManyCart([FromBody] ICollection<string> ids)
        {
            var response = await _provider.DeleteManyCart(ids);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
