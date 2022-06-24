using System.Threading.Tasks;
using System.Collections.Generic;
using Console.ApplicationCore.Dtos;
using Console.ApplicationCore.Entities;

namespace Console.ApplicationCore.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);

        Task<User> GetByUsernameAsync(string username);

        Task<User> GetByEmailAsync(string email);

        Task<ResultDto<User>> VerifyUserSignInAsync(LoginDto dto);

        Task<ResultDto<User>> RegisterUserAsync(RegisterDto dto);
    }
}
