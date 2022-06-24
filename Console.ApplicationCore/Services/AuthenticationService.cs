using Console.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Console.ApplicationCore.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string PROVIDER = "JWT_BEARER";
        private const string NAME = "REFRESH_TOKEN";
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> SigninAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var accessToken = _tokenService.GetAccessToken(claims);

            var refreshToken = _tokenService.GetRefreshToken(claims);

            await _userManager.SetAuthenticationTokenAsync(user, PROVIDER, NAME, refreshToken);

            return accessToken;
        }
    }
}
