using MediatR;

namespace DirectoryApp.Application.Command.RemoveContactInformation;

public class RemoveContactInformationCommand : IRequest<RemoveContactInformationResponse>
{
    public Guid InformationId { get; set; }
}