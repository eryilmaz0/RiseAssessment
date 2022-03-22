using MediatR;

namespace DirectoryApp.Application.Command.UpdateEnrollee;

public class UpdateEnrolleeCommand : IRequest<UpdateEnrolleeResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Firm { get; set; }
}