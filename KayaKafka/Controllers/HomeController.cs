using KafkaServices.Producer;
using Microsoft.AspNetCore.Mvc;

namespace KayaKafka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMessageProducer messageProducer;

        public HomeController(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await messageProducer.SendMessageAsync("topic", "any message :)");
            return Ok("Message");
        }
    }
}
