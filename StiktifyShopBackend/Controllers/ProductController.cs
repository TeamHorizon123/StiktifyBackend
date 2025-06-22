using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StiktifyShopBackend.Interfaces;

namespace StiktifyShopBackend.Controllers
{
    [Route("odata/product")]
    [ApiController]
    public class ProductController : ODataController
    {
        private IProductProvider _provider;

        public ProductController(IProductProvider provider)
        {
            _provider = provider ?? throw new ArgumentException(nameof(_provider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProduct>> GetAll()
        {
            var list = _provider.GetAll().AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/shop")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProduct>> GetAllOfShop([FromRoute] string id)
        {
            var list = _provider.GetAllOfShop(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("{id}/category")]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseProduct>> GetAllOfCategory([FromRoute] string id)
        {
            var list = _provider.GetAllOfCategory(id).AsQueryable();
            return Ok(list);
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetAllImage([FromRoute] string id)
        {
            var list = await _provider.GetAllImage(id);
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var product = await _provider.GetOne(id);
            return product?.Id == null ? NotFound() : Ok(product);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] RequestCreateProduct request)
        {
            var response = await _provider.Create(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] RequestUpdateProduct request)
        {
            if (id != request.Id)
                return BadRequest("Id does not match.");
            var response = await _provider.Update(request);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            var response = await _provider.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("delete-many")]
        public async Task<IActionResult> DeleteMany([FromRoute] ICollection<string> ids)
        {
            var response = await _provider.DeleteMany(ids);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
