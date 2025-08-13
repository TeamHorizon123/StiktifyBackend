using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShop.Application.DTOs.Requests;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;

namespace StiktifyShop.Controllers
{
    [Route("odata/user-address")]
    [ApiController]
    [Authorize]
    public class UserAddressController : ODataController
    {
        private IUserAddressRepo _repo;
        public UserAddressController(IUserAddressRepo repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseUserAddress>> GetAll()
        {
            var listAddressses = _repo.GetAll().AsQueryable();
            return Ok(listAddressses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateUserAddress address)
        {
            if (address == null)
            {
                return BadRequest("Invalid address data.");
            }
            var response = await _repo.Create(address);
            if (response.StatusCode == 201)
            {
                return StatusCode(response.StatusCode, response.Data);
            }
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] string id, [FromBody] UpdateUserAddress address)
        {
            if (address == null || id != address.Id)
            {
                return BadRequest("Invalid address data.");
            }
            var response = await _repo.Update(address);
            if (response.StatusCode == 200)
            {
                return Ok(response.Data);
            }
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid address ID.");
            }
            var response = await _repo.Delete(id);
            if (response.StatusCode == 204)
            {
                return NoContent();
            }
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
