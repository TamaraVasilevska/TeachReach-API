using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeachReach.TeachReach.Domain.Entities;
using TeachReach.TeachReach.Application.Services.Interfaces;

namespace TeachReach.TeachReach.Application.Services.Implementation
{
    public class StudentAuthenticationService : IStudentAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentService _studentService;

        public StudentAuthenticationService(IHttpContextAccessor httpContextAccessor, IStudentService studentService)
        {
            _httpContextAccessor = httpContextAccessor;
            _studentService = studentService;
        }

        public async Task<Student> Authenticate(string email, string password)
        {
            // Retrieve student by email
            var student = await _studentService.GetByEmail(email);

            // Check if student exists and password is valid
            if (student != null && IsPasswordValid(student, password))
            {
                // Sign in the student
                await SignIn(student);
                return student;
            }

            // Authentication failed
            return null;
        }

        public async Task SignOut()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task SignIn(Student student)
        {
            // Create claims for the student
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Email, student.Email),
                // Add more claims as needed
            };

            // Create ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create authentication properties
            var authProperties = new AuthenticationProperties
            {
                // Configure additional properties if needed
            };

            // Sign in the student
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private bool IsPasswordValid(Student student, string password)
        {
            // Add your password validation logic here
            // For example, compare hashed passwords
            // return student.PasswordHash == Hash(password);
            // Dummy implementation
            return student.Password == password;
        }
    }
}
