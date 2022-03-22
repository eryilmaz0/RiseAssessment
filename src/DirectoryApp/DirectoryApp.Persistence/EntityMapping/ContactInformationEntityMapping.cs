using DirectoryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryApp.Persistence.EntityMapping;

public class ContactInformationEntityMapping : IEntityTypeConfiguration<ContactInformation>
{
    public void Configure(EntityTypeBuilder<ContactInformation> builder)
    {
        builder.Property(x => x.InformationType).IsRequired();
        builder.Property(x => x.Information).IsRequired().HasMaxLength(100);
        builder.Property(x => x.EnrolleeId).IsRequired();
    }
}