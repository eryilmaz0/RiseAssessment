using DirectoryApp.Domain.Entity;
using DirectoryApp.Persistence.EntityMapping;
using Microsoft.EntityFrameworkCore;

namespace DirectoryApp.Persistence.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> opts):base(opts)
    {
        
    }


    public DbSet<Enrollee> Enrollees { get; set; }
    public DbSet<ContactInformation> ContactInformations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EnrolleeEntityMapping());
        modelBuilder.ApplyConfiguration(new ContactInformationEntityMapping());
    }
}