using Microsoft.AspNetCore.Mvc;

namespace crypto_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Content("Hello User");
        }
        
    }
}