using Microsoft.Extensions.DependencyInjection;
using Console.ApplicationCore.Services;

namespace Console.ApplicationCore
{
    public static class Startup
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();  
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
