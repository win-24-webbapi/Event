using System;
using System.ComponentModel.DataAnnotations;

namespace EventService.API.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        public int CurrentParticipants { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
} 