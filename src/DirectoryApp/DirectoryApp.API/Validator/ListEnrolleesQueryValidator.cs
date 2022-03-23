using DirectoryApp.Application.Query.ListEnrollees;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class ListEnrolleesQueryValidator : AbstractValidator<ListEnrolleesQuery>
{
    public ListEnrolleesQueryValidator()
    {
        
    }
}