using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using Console.Module.Localization.Configurations;
using System.Linq; 
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Console.Module.Localization
{
    public static class Startup
    {
        public static IServiceCollection AddLocalizationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLocalization();

            services.AddSingleton<IStringLocalizerFactory, JsonStringlocalizerFactory>();

            var config = new LocalizationConfig();
            configuration.GetSection("Localization").Bind(config);

            var supportedCultures = config.SupportedCultures.Select(p => new CultureInfo(p)).ToList();  

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.DefaultRequestCulture = new RequestCulture(config.DefaultCulture, config.DefaultCulture);
            });

            return services;
        }

        public static IApplicationBuilder UseLocalizationModule(this IApplicationBuilder app)
        {
            app.UseRequestLocalization();

            return app;
        }
    }
}
