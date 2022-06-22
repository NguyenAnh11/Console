using Console.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Console.Infrastructure.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Address)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.HasIndex(p => p.PhoneNumber).IsUnique().HasDatabaseName("PhoneIndex");
        }
    }
}
