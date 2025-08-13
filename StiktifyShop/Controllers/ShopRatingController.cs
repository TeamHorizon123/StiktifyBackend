using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Infrastructure.Repository;

namespace StiktifyShop.Controllers
{
    [Route("odata/shop-rating")]
    [ApiController]
    //[Authorize]
    public class ShopRatingController : ODataController
    {
        private ShopRatingRepo _repo;

        public ShopRatingController(ShopRatingRepo repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ResponseShopRating>> GetAll([FromRoute] string id)
        {
            var listRating = _repo.GetAll(id);
            return Ok(listRating.AsQueryable());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            var shopRating = await _repo.Get(id);
            return shopRating == null ? NotFound() : Ok(new { value = shopRating });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShopRating rating)
        {
            var response = await _repo.Create(rating);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return Created(response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
