using Microsoft.EntityFrameworkCore;
using EventService.API.Models;

namespace EventService.API.Data
{
    public class EventServiceDbContext : DbContext
    {
        public EventServiceDbContext(DbContextOptions<EventServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
                entity.Property(e => e.MaxParticipants).IsRequired();
                entity.Property(e => e.OrganizerId).IsRequired();
                
                // Add indexes for faster queries
                entity.HasIndex(e => e.OrganizerId);
                entity.HasIndex(e => e.StartDate);
                entity.HasIndex(e => e.IsPublished);
            });
        }
    }
} 