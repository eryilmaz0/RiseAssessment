namespace ReportGenerator.Entity;

public class Enrollee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Firm { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<ContactInformation> ContactInformations { get; set; }


    public Enrollee()
    {
        this.Created = DateTime.UtcNow;
        this.IsActive = true;
        this.ContactInformations = new List<ContactInformation>();
    }


}