using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ODataController
    {
        private IOrderRepo _repo;
        public OrderController(IOrderRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrder>> GetAll()
        {
            var listOrder = _repo.GetAll().AsQueryable();
            return Ok(listOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var order = await _repo.Get(id);
            return order == null ? NotFound() : Ok(new { value = order });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrder order)
        {
            var response = await _repo.Create(order);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateOrder order)
        {
            if (id != order.Id)
                return BadRequest("Order ID mismatch.");
            var response = await _repo.Update(order);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

    }
}
