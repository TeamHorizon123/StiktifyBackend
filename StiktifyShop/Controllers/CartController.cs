using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace StiktifyShop.Controllers
{
    [Route("odata/cart")]
    [ApiController]
    [Authorize]
    public class CartController : ODataController
    {
    }
}
