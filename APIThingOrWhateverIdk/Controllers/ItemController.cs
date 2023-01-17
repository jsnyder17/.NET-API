using APIThingOrWhateverIdk.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIThingOrWhateverIdk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IConfiguration _configuration;

        public ItemController(ILogger<PersonController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("~/getallitems")]
        public List<ItemModel> GetAllItems()
        {
            List<ItemModel> items = DatabaseController.GetAllItems();
            return items;
        }

        [HttpPost("~/insertitem")]
        public HttpResponseMessage InsertItems(List<ItemModel> items)
        {
            if (items == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                if (DatabaseController.InsertItem(items))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet("~/insertsampleitems")]
        public HttpResponseMessage InsertSampleItems()
        {
            List<ItemModel> items = new List<ItemModel>();

            items.Add(new ItemModel { Name = "64GB USB", Type = "E", Price = 19.99, Quantity = 32 });
            items.Add(new ItemModel { Name = "Mechanical Keyboard", Type = "E", Price = 99.99, Quantity = 16 });
            items.Add(new ItemModel { Name = "32bit EEPROM", Type = "E", Price = 1.99, Quantity = 255 });

            if (DatabaseController.InsertItem(items))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
