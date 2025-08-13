using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Helper;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController : ODataController
    {
        private IPaymentRepo repo;
        private readonly IVNPayHelper VNPayHelper;
        private readonly IConfiguration config;
        public PaymentController(IPaymentRepo repo, IVNPayHelper vNPayHelper, IConfiguration config)
        {
            this.repo = repo ?? throw new ArgumentException(nameof(repo));
            VNPayHelper = vNPayHelper;
            this.config = config;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponsePayment>> GetAll()
        {
            var payments = repo.GetAll();
            return Ok(payments.AsQueryable());
        }

        [HttpGet("vnpay-return")]
        [AllowAnonymous]
        public ActionResult VNPayReturn()
        {
            var response = VNPayHelper.PaymentExecute(HttpContext.Request.Query);
            return response.VnPayResponseCode == "00" ? Redirect(config["Redirect:UrlSuccess"]!) : BadRequest(config["Redirect:UrlFail"]!);
        }


        [HttpPost("create-vnpay")]
        [AllowAnonymous]
        public IActionResult CreateVNPay([FromBody] VNPayRequest request)
        {
            var paymentUrl = VNPayHelper.CreatePaymentUrl(request, HttpContext);
            return Ok(new { url = paymentUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePayment createPayment)
        {
            var response = await repo.Create(createPayment);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatePayment updatePayment)
        {
            if (id != updatePayment.Id)
            {
                return BadRequest("Payment ID mismatch.");
            }
            var response = await repo.Update(updatePayment);
            return StatusCode(response.StatusCode, response.Data);
        }

    }
}
