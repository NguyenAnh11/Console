using Console.ApplicationCore.Interfaces;
using Console.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Console.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(p => p.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetPrecision(18);
                property.SetScale(6);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ISoftDelete)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Property("IsDelete").CurrentValue = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.Property("IsDelete").CurrentValue = true;
                            break;
                    }
                }

                if (entry.Entity is IAuditable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Property("CreateAtUtc").CurrentValue = DateTime.UtcNow;
                            break;
                        case EntityState.Modified or EntityState.Deleted:
                            entry.Property("LastModifiedAtUtc").CurrentValue = DateTime.UtcNow;
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
