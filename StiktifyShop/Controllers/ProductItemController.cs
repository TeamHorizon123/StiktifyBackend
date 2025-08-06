using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace StiktifyShop.Controllers
{
    [Route("odata/product-item")]
    [ApiController]
    [Authorize]
    public class ProductItemController : ODataController
    {
    }
}
