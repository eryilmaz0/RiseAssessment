using MediatR;

namespace DirectoryApp.Application.Command.CreateEnrollee;

public class CreateEnrolleeCommand : IRequest<CreateEnrolleeResponse>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Firm { get; set; }
}