using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;
using System.Threading.Tasks;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/shop")]
    [ApiController]
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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetOfUser([FromRoute] string id)
        {
            var shop = await _provider.GetOfUser(id);
            return shop == null ? NotFound() : Ok(shop);
        }

        [HttpGet("shop/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var shop = await _provider.GetOne(id);
            return shop == null ? NotFound() : Ok(shop);
        }

        [HttpPost("create-shop")]
        public async Task<IActionResult> CreateShop([FromBody] RequestCreateShop createShop)
        {
            var response = await _provider.CreateShop(createShop);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update-shop/{id}")]
        public async Task<IActionResult> UpdateShop([FromRoute] string id, [FromBody] RequestUpdateShop updateShop)
        {
            if (id != updateShop.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateShop(updateShop);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("Delete-shop/{id}")]
        public async Task<IActionResult> DeleteShop([FromRoute] string id)
        {
            var response = await _provider.DeleteShop(id);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
