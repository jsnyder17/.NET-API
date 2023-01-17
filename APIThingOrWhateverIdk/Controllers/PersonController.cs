using APIThingOrWhateverIdk.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIThingOrWhateverIdk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IConfiguration _configuration;

        public PersonController(ILogger<PersonController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("~/getallpersons")]
        public List<PersonModel> GetAllPersons()
        {
            List<PersonModel> persons = DatabaseController.GetAllPersons();
            return persons;
        }

        [HttpPost("~/insertperson")]
        public HttpResponseMessage InsertPerson(List<PersonModel> persons)
        {
            if (persons == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                if (DatabaseController.InsertPerson(persons))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }
        }
        
        [HttpGet("~/insertsamplepersons")]
        public HttpResponseMessage InsertSamplePersons()
        {
            List<PersonModel> persons = new List<PersonModel>();

            persons.Add(new PersonModel { Name = "Josh", Age = 22 });
            persons.Add(new PersonModel { Name = "Liam", Age = 22 });
            persons.Add(new PersonModel { Name = "Robby", Age = 21 });

            if (DatabaseController.InsertPerson(persons))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
