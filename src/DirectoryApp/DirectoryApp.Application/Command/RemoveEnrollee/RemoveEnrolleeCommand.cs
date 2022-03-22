using MediatR;

namespace DirectoryApp.Application.Command.RemoveEnrollee;

public class RemoveEnrolleeCommand : IRequest<RemoveEnrolleeResponse>
{
    public Guid EnrolleeId { get; set; }
}