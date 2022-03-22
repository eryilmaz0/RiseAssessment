using MediatR;

namespace DirectoryApp.Application.Query.GetEnrolleeWithDetail;

public class GetEnrolleeWithDetailQuery : IRequest<GetEnrolleeWithDetailViewModel>
{
    public Guid EnrolleeId { get; set; }
}