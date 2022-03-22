using DirectoryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryApp.Persistence.EntityMapping;

public class EnrolleeEntityMapping : IEntityTypeConfiguration<Enrollee>
{
    public void Configure(EntityTypeBuilder<Enrollee> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Firm).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.ContactInformations).WithOne(x => x.Enrollee).HasForeignKey(x => x.EnrolleeId);
    }
}