using Console.ApplicationCore.Dtos;
using Console.ApplicationCore.Entities;
using Console.Module.Localization.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Console.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<UserService> _localizer; 

        public UserService(
            UserManager<User> userManager,
            IStringLocalizer<UserService> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;

            var user = await _userManager.FindByIdAsync(id.ToString());

            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            if (username == null || username.Trim().Length == 0)
                throw new ArgumentNullException(nameof(username));

            var user = await _userManager.FindByNameAsync(username);

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            if (email == null || email.Trim().Length == 0)
                throw new ArgumentNullException(nameof(email));

            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<ResultDto<User>> VerifyUserSignInAsync(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await GetByEmailAsync(dto.Email);

            if (user == null)
                return ResultDto<User>.Bad(_localizer[AccountConstrant.Account_Error_UserNotFound]);

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return ResultDto<User>.Bad(_localizer[AccountConstrant.Account_Error_WrongCredential]);

            return ResultDto<User>.Ok(user);
        }

        public async Task<ResultDto<User>> RegisterUserAsync(RegisterDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await GetByEmailAsync(dto.Email);

            if (user != null)
                return ResultDto<User>.Bad(_localizer[AccountConstrant.Account_Error_EmailAlreadyUsed]);

            user = await GetByUsernameAsync(dto.UserName);

            if (user != null)
                return ResultDto<User>.Bad(_localizer[AccountConstrant.Account_Error_EmailAlreadyUsed]);

            user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            await _userManager.CreateAsync(user, dto.Password);

            return ResultDto<User>.Ok(user);
        }
    }
}
