using Console.ApplicationCore.Configurations;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Console.ApplicationCore.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfig _config;

        public TokenService(TokenConfig config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(IList<Claim> claims, int expireInSecond)
        {
            if (claims == null || !claims.Any())
                throw new ArgumentNullException(nameof(claims));

            if (expireInSecond <= 0)
                throw new Exception("Expire time must be greater than 0");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));

            var tokenHandler = new JwtSecurityTokenHandler();

            var expires = DateTime.Now.AddSeconds(expireInSecond);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.Issuer,
                Audience = _config.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                IssuedAt = expires,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public ClaimsPrincipal VerifyJSONWebToken(string token)
        {
            if (token == null || token.Trim().Length == 0)
                throw new ArgumentNullException(nameof(token));

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));

                var tokenHandler = new JwtSecurityTokenHandler();

                var parameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateIssuer = _config.ValidateIssuer,
                    ValidateAudience = _config.ValidateAudience,
                    ValidIssuer = _config.Issuer,
                    ValidAudience = _config.Audience,
                    ValidateIssuerSigningKey = _config.ValidateIssuerSigningKey,
                    ValidateLifetime = _config.ValidateLifetime,
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string GetAccessToken(IList<Claim> claims)
            => GenerateJSONWebToken(claims, _config.AccessTokenExpireInSecond);

        public string GetRefreshToken(IList<Claim> claims)
            => GenerateJSONWebToken(claims, _config.RefreshTokenExpireInSecond);
    }
}
