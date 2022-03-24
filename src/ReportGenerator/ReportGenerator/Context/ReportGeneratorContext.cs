using Microsoft.EntityFrameworkCore;
using ReportGenerator.Entity;

namespace ReportGenerator.Context;

public class ReportGeneratorContext : DbContext
{
    public ReportGeneratorContext(DbContextOptions<ReportGeneratorContext> opts):base(opts)
    {
        
    }

    public DbSet<Enrollee> Enrollees { get; set; }
    public DbSet<ContactInformation> ContactInformations  { get; set; }
}