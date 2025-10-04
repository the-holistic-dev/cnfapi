using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}
