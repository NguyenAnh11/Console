using Console.ApplicationCore.Dtos;
using Console.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace Console.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;

            var user = await _userManager.FindByIdAsync(id.ToString());

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            if (email == null || email.Trim().Length == 0)
                throw new ArgumentNullException(nameof(email));

            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User> VerifyUserSignInAsync(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await GetByEmailAsync(dto.Email);

            if (user == null)
                return null;

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            return user;
        }
    }
}
