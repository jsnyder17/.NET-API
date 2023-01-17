using Microsoft.AspNetCore.Mvc;

namespace APIThingOrWhateverIdk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseManagementController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IConfiguration _configuration;

        public DatabaseManagementController(ILogger<PersonController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("~/resetdatabase")]
        public HttpResponseMessage ResetDatabase()
        {
            if (DatabaseController.ResetDatabase())
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
