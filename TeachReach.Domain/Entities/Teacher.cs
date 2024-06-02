using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace TeachReach.TeachReach.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? City { get; set; }
        public List<Subject>? Subjects { get; set; } = new List<Subject>();
        [Required]
        public string PhoneNumber { get; set; }
        public int Experience { get; set; } = 0;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public decimal HourlyRate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public List<Review>? Reviews { get; set; } = new List<Review>();
    }
}
