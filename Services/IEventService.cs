using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventService.API.Models;

namespace EventService.API.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventModel);
        Task<Event> UpdateEventAsync(int id, Event eventModel);
        Task DeleteEventAsync(int id);
    }
} 