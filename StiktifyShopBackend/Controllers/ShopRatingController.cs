using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;
using System.Threading.Tasks;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/shop-rating")]
    [ApiController]
    public class ShopRatingController : ODataController
    {
        private IShopRatingProvider _provider;

        public ShopRatingController(IShopRatingProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseShopRating>> GetAllOfShop([FromRoute] string id)
        {
            var list = _provider.GetAllOfShop(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var rating = await _provider.GetOne(id);
            return rating?.Id == null ? NotFound() : Ok(rating);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateShopRating([FromBody] RequestCreateShopRating request)
        {
            var response = await _provider.CreateShopRating(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateShopRating([FromRoute] string id, [FromBody] RequestUpdateShopRating request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateShopRating(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteShopRating([FromRoute] string id)
        {
            var response = await _provider.DeleteShopRating(id);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
