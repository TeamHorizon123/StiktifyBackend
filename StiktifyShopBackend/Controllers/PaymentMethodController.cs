using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/payment-method")]
    [ApiController]
    public class PaymentMethodController : ODataController
    {
        private IPaymentMethodProvider _provider;

        public PaymentMethodController(IPaymentMethodProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponsePayment>> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var payment = await _provider.GetOne(id);
            return payment?.Id == null ? NotFound() : Ok(payment);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMethod([FromBody] RequestCreateMethod request)
        {
            var response = await _provider.Create(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] string id, [FromBody] RequestUpdateMethod request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.Create(request);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
