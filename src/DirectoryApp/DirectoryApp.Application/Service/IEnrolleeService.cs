using DirectoryApp.Application.Command.CreateEnrollee;
using DirectoryApp.Application.Command.RemoveEnrollee;
using DirectoryApp.Application.Command.RemoveEnrolleeFromDirectory;
using DirectoryApp.Application.Command.UpdateEnrollee;
using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using DirectoryApp.Application.Query.ListEnrollees;
using MediatR;

namespace DirectoryApp.Application.Service;

public interface IEnrolleeService : IRequestHandler<CreateEnrolleeCommand, CreateEnrolleeResponse>,
                                    IRequestHandler<UpdateEnrolleeCommand, UpdateEnrolleeResponse>,
                                    IRequestHandler<RemoveEnrolleeCommand, RemoveEnrolleeResponse>,
                                    IRequestHandler<ListEnrolleesQuery, ICollection<ListEnrolleesViewModel>>,
                                    IRequestHandler<GetEnrolleeWithDetailQuery, GetEnrolleeWithDetailViewModel>,
                                    IRequestHandler<RemoveEnrolleeFromDirectoryCommand, RemoveEnrolleeFromDirectoryResponse>
{
    
}