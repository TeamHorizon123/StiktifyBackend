using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/product-rating")]
    [ApiController]
    public class ProductRatingController : ODataController
    {
        private IProductRatingProvider _provider;

        public ProductRatingController(IProductRatingProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<ResponseProductRating> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<ResponseProductRating> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/option")]
        [EnableQuery]
        public ActionResult<ResponseProductRating> GetAllOfOption([FromRoute] string id)
        {
            var list = _provider.GetAllOfOption(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var rating = await _provider.GetOne(id);
            return rating?.Id == null ? NotFound() : Ok(rating);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRating([FromBody] RequestCreateRating request)
        {
            var response = await _provider.CreateRating(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRating([FromRoute] string id, [FromBody] RequestUpdateRating request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateRating(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRating([FromRoute] string id)
        {
            var response = await _provider.DeleteRating(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
