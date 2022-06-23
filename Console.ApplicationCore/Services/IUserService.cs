using System.Threading.Tasks;
using Console.ApplicationCore.Entities;

namespace Console.ApplicationCore.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
    }
}
