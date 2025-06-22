using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/order-tracking")]
    [ApiController]
    public class OrderTrackingController : ODataController
    {
        private IOrderTrackingProvider _provider;

        public OrderTrackingController(IOrderTrackingProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrderTracking>> GetAllOfOrder([FromRoute] string id)
        {
            var list = _provider.GetAllOfOrder(id).AsQueryable();
            return Ok(list);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTracking([FromBody] RequestCreateTracking request)
        {
            var response = await _provider.CreateTracking(request);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
