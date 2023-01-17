using Microsoft.AspNetCore.Mvc;

namespace APIThingOrWhateverIdk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiscController : Controller 
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IConfiguration _configuration;

        public MiscController(ILogger<PersonController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("~/ping")]
        public HttpResponseMessage Ping()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK); ;
        }
    }
}
