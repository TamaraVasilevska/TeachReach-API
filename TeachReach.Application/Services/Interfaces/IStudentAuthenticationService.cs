using System.Threading.Tasks;
using TeachReach.TeachReach.Domain.Entities;

namespace TeachReach.TeachReach.Application.Services.Interfaces
{
    public interface IStudentAuthenticationService
    {
        Task<Student> Authenticate(string email, string password);
        Task SignOut();
    }
}
