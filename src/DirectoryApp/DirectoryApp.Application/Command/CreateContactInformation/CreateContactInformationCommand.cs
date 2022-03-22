using DirectoryApp.Domain.Enum;
using MediatR;

namespace DirectoryApp.Application.Command.CreateContactInformation;

public class CreateContactInformationCommand : IRequest<CreateContactInformationResponse>
{
    public Guid EnrolleeId { get; set; }
    public InformationType InformationType { get; set; }
    public string Information { get; set; }
}