using DirectoryApp.Domain.Enum;

namespace DirectoryApp.Application.Query.GetEnrolleeWithDetail;

public class GetEnrolleeWithDetailViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Firm { get; set; }
    public List<ContactInformationViewModel> ContactInformations { get; set; }
}


public class ContactInformationViewModel
{
    public InformationType InformationType { get; set; }
    public string Information { get; set; }
    public Guid EnrolleeId { get; set; }
}