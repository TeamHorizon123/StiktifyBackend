using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace StiktifyShop.Controllers
{
    [Route("odata/order-tracking")]
    [ApiController]
    public class OrderTrackingController : ODataController
    {
    }
}
