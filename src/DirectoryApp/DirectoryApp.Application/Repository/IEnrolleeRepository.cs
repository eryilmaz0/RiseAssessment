using DirectoryApp.Application.Command.CreateEnrollee;
using DirectoryApp.Domain.Entity;
using MediatR;

namespace DirectoryApp.Application.Repository;

public interface IEnrolleeRepository : IRepository<Enrollee, Guid>
{
    
}