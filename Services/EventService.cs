using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventService.API.Models;
using EventService.API.Data;
using Microsoft.EntityFrameworkCore;

namespace EventService.API.Services
{
    public class EventService : IEventService
    {
        private readonly EventServiceDbContext _context;

        public EventService(EventServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event eventModel)
        {
            eventModel.CreatedAt = DateTime.UtcNow;
            eventModel.UpdatedAt = DateTime.UtcNow;
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<Event> UpdateEventAsync(int id, Event eventModel)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return null;

            existingEvent.Title = eventModel.Title;
            existingEvent.Description = eventModel.Description;
            existingEvent.StartDate = eventModel.StartDate;
            existingEvent.EndDate = eventModel.EndDate;
            existingEvent.Location = eventModel.Location;
            existingEvent.MaxParticipants = eventModel.MaxParticipants;
            existingEvent.CurrentParticipants = eventModel.CurrentParticipants;
            existingEvent.OrganizerId = eventModel.OrganizerId;
            existingEvent.ImageUrl = eventModel.ImageUrl;
            existingEvent.IsPublished = eventModel.IsPublished;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingEvent;
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
} 