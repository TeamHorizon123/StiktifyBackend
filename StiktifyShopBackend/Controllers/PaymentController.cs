using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/payment")]
    [ApiController]
    public class PaymentController : ODataController
    {
        private IPaymentProvider _provider;

        public PaymentController(IPaymentProvider provider)
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

        [HttpGet("{id}/user")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponsePayment>> GetAllOfUser([FromRoute] string id)
        {
            var list = _provider.GetAllOfProduct(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/product")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponsePayment>> GetAllOfProduct([FromRoute] string id)
        {
            var list = _provider.GetAllOfUser(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] string id)
        {
            var payment = await _provider.GetOne(id);
            return payment?.Id == null ? NotFound() : Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] RequestCreatePayment request)
        {
            var response = await _provider.Create(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePayment([FromRoute]string id, [FromBody] RequestUpdatePayment request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.Create(request);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
