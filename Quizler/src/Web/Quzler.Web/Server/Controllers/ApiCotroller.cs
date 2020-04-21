namespace Quzler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiCotroller : ControllerBase
    {
    }
}
