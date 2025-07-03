using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/shop")]
    [ApiController]
    [Authorize]
    public class ShopController : ODataController
    {
        private IShopProvider _provider;

        public ShopController(IShopProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseShop>> GetAll()
        {
            var listShop = _provider.GetAll();
            return Ok(listShop.AsQueryable());
        }

        [HttpGet("owner/{id}")]
        public async Task<IActionResult> GetOfUser([FromRoute] string id)
        {
            var shop = await _provider.GetOfUser(id);
            return shop == null ? NotFound() : Ok(new { shop });
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var shop = await _provider.GetOne(id);
            return shop == null ? NotFound() : Ok(new { shop });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateShop([FromBody] RequestCreateShop createShop)
        {
            var response = await _provider.CreateShop(createShop);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(201, new { id = response.Message });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateShop([FromRoute] string id, [FromBody] RequestUpdateShop updateShop)
        {
            if (id != updateShop.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateShop(updateShop);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode, new { id = response.Message });
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteShop([FromRoute] string id)
        {
            var response = await _provider.DeleteShop(id);
            if (response.StatusCode != 204)
                return StatusCode(response.StatusCode, new { message = response.Message });
            return StatusCode(response.StatusCode);
        }
    }
}
