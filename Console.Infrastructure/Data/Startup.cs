using Console.ApplicationCore.Entities;
using Console.ApplicationCore.Repositories;
using Console.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Infrastructure.Data
{
    public static class Startup
    {
        public static IServiceCollection AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var connectionString = configuration.GetConnectionString("Dev");
            var migrationAssembly = typeof(AppDbContext).Assembly.GetName().Name;
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString, option => option.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
