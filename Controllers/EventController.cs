using Microsoft.AspNetCore.Mvc;
using EventService.API.Models;
using EventService.API.Services;

namespace EventService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventService.GetEventsAsync();
            Console.WriteLine($"Returning {events.Count()} events");
            foreach (var evt in events)
            {
                Console.WriteLine($"Event: {evt.Title}, Price: {evt.Price}, MaxParticipants: {evt.MaxParticipants}");
            }
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventModel = await _eventService.GetEventByIdAsync(id);
            if (eventModel == null)
                return NotFound();

            return Ok(eventModel);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event eventModel)
        {
            var createdEvent = await _eventService.CreateEventAsync(eventModel);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Event>> UpdateEvent(int id, Event eventModel)
        {
            var updatedEvent = await _eventService.UpdateEventAsync(id, eventModel);
            if (updatedEvent == null)
                return NotFound();

            return Ok(updatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
} 