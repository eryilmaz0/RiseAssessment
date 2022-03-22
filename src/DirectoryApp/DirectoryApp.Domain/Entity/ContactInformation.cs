using DirectoryApp.Domain.Enum;

namespace DirectoryApp.Domain.Entity;

public class ContactInformation : Entity<Guid>
{
    public InformationType InformationType { get; set; }
    public Guid EnrolleeId { get; set; }
    public string Information { get; set; }

    public virtual Enrollee Enrollee { get; set; }
}