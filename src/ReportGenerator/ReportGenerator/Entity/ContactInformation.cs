using ReportGenerator.Enum;

namespace ReportGenerator.Entity;

public class ContactInformation
{
    public Guid Id { get; set; }
    public InformationType InformationType { get; set; }
    public Guid EnrolleeId { get; set; }
    public string Information { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }

    public virtual Enrollee Enrollee { get; set; }

    public ContactInformation()
    {
        this.Created = DateTime.UtcNow;
        this.IsActive = true;
    }
}