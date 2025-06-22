using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/order-detail")]
    [ApiController]
    public class OrderDetailController : ODataController
    {
        private IOrderDetailProvider _provider;

        public OrderDetailController(IOrderDetailProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var detail = await _provider.GetOne(id);
            return detail?.Id == null ? NotFound() : Ok(detail);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDetail([FromBody] RequestCreateOrderDetail request)
        {
            var response = await _provider.CreateDetail(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDetail(int id, [FromBody] RequestUpdateOrderDetail request)
        {
            var response = await _provider.UpdateDetail(request);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
