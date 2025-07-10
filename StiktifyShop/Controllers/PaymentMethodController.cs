using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/payment-method")]
    [ApiController]
    //[Authorize]
    public class PaymentMethodController : ODataController
    {
        private IPaymentMethodRepo _repo;
        public PaymentMethodController(IPaymentMethodRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponsePaymentMethod>> GetAll()
        {
            var listPaymentMethod = _repo.GetAll().AsQueryable();
            return Ok(listPaymentMethod);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var paymentMethod = await _repo.Get(id);
            return paymentMethod == null ? NotFound() : Ok(new { value = paymentMethod });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentMethod method)
        {
            var response = await _repo.Create(method);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatePaymentMethod method)
        {
            if (id != method.Id)
                return BadRequest("Payment method ID mismatch.");
            var response = await _repo.Update(method);
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
