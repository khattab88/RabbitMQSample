using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Services;
using RabbitMQSample.Models;

namespace Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;

        // in-memory db
        public static readonly List<Booking> _bookings = new();

        public BookingsController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost(Name = "")]
        public ActionResult CreateBooking(Booking booking) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _bookings.Add(booking);

            _messageProducer.SendMessage<Booking>(booking);

            return Ok();
        }
    }
}
