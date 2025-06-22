using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/address")]
    [ApiController]
    public class AddressController : ODataController
    {
        private IAddressProvider _provider;

        public AddressController(IAddressProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseReceiveAddress>> GetAll([FromRoute] string id)
        {
            var list = _provider.GetAllOfUser(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var address = await _provider.GetOne(id);
            return address?.Id == null ? NotFound() : Ok(address);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAddress([FromBody] RequestCreateAddress request)
        {
            var response = await _provider.CreateAddress(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] string id, [FromBody] RequestUpdateAddress request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.UpdateAddress(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] string id)
        {
            var response = await _provider.DeleteAddress(id);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
