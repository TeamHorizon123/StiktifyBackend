using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/order")]
    [ApiController]
    public class OrderController : ODataController
    {
        private IOrderProvider _provider;

        public OrderController(IOrderProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}/shop")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrder>> GetAllOfShop([FromRoute] string id)
        {
            var list = _provider.GetAllOfShop(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/user")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrder>> GetAllOfUser([FromRoute] string id)
        {
            var list = _provider.GetAllOfUser(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrder>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var order = await _provider.GetOne(id);
            return order?.Id == null ? NotFound() : Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] RequestCreateOrder request)
        {
            var response = await _provider.CreateOrder(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]string id, [FromBody] RequestUpdateOrder request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateOrder(request);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
