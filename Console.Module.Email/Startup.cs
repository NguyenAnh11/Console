using Console.ApplicationCore.Services;
using Console.Module.Email.Configurations;
using Console.Module.Email.Services.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Module.Email
{
    public static class Startup
    {
        public static IServiceCollection AddEmailModule(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new EmailConfig();

            configuration.GetSection("Email").Bind(config);

            if (config.Provider == "Smtp")
            {
                services.AddScoped<IEmailService, SmtpEmailService>();
            }

            return services;
        }
    }
}
