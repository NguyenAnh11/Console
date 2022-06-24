using Console.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Console.ApplicationCore.Services
{
    public interface IAuthenticationService
    {
        Task<string> SigninAsync(User user);
    }
}
