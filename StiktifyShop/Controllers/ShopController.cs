using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/shop")]
    [ApiController]
    //[Authorize]
    public class ShopController : ODataController
    {
        private IShopRepo _repo;
        public ShopController(IShopRepo repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }


        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseShop>> GetAll()
        {
            var listShop = _repo.GetAll().AsQueryable();
            return Ok(listShop);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseShop>> GetOne([FromRoute] string id)
        {
            var shop = await _repo.Get(id);
            return shop == null ? NotFound() : Ok(new { value = shop });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShop shop)
        {
            var response = await _repo.Create(shop);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateShop shop)
        {
            if (id != shop.Id)
                return BadRequest("Shop ID mismatch.");
            var response = await _repo.Update(shop);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
