using DirectoryApp.Application.Command.CreateContactInformation;
using DirectoryApp.Application.Command.RemoveContactInformation;
using MediatR;

namespace DirectoryApp.Application.Service;

public interface IContactInformationService : IRequestHandler<CreateContactInformationCommand, CreateContactInformationResponse>,
                                              IRequestHandler<RemoveContactInformationCommand, RemoveContactInformationResponse>
{
    
}