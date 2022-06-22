using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Console.Module.Email.Configurations;
using Console.Module.Email.Services.Smtp;
using Console.Module.Email.Services.SendGrid;
using Console.Module.Email.Services;

namespace Console.Module.Email
{
    public static class Startup
    {
        public static IServiceCollection AddEmailModule(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = new EmailConfig();

            configuration.GetSection("Email").Bind(emailConfig);

            services.AddSingleton(emailConfig);   

            if(emailConfig.Provider == "Smtp")
            {
                services.AddScoped<IEmailService, SmtpEmailService>();
            }
            else if(emailConfig.Provider == "SendGrid")
            {
                services.AddScoped<IEmailService, SendGridEmailService>();
            }

            return services;
        }
    }
}
