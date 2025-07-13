using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/product-rating")]
    [ApiController]
    //[Authorize]
    public class ProductRatingController : ODataController
    {
        private IProductRatingRepo _repo;
        public ProductRatingController(IProductRatingRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProductRating>> GetAll([FromRoute] string id)
        {
            var listRating = _repo.GetAll(id).AsQueryable();
            return Ok(listRating);
        }

        [HttpGet("{id}/get")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var productRating = await _repo.Get(id);
            return productRating == null ? NotFound() : Ok(new { value = productRating });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
