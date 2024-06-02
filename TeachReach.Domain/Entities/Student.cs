using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeachReach.TeachReach.Domain.Entities
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
