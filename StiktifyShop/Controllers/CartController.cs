using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/cart")]
    [ApiController]
    //[Authorize]
    public class CartController : ODataController
    {
        private ICartRepo _repo;
        public CartController(ICartRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCart>> GetAll()
        {
            var listCart = _repo.GetAll().AsQueryable();
            return Ok(listCart);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCart cart)
        {
            var response = await _repo.Create(cart);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCart cart)
        {
            if (id != cart.Id)
                return BadRequest("Cart ID mismatch.");
            var response = await _repo.Update(cart);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete-many")]
        public async Task<IActionResult> DeleteItem([FromBody] ICollection<DeleteCart> cartIds)
        {
            if (cartIds == null || !cartIds.Any())
                return BadRequest("No cart IDs provided.");
            var response = await _repo.DeleteMany(cartIds);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
