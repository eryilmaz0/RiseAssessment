using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class GetEnrolleeWithDetailQueryValidator : AbstractValidator<GetEnrolleeWithDetailQuery>
{
    public GetEnrolleeWithDetailQueryValidator()
    {
        RuleFor(x => x.EnrolleeId).NotEmpty().WithMessage("EnrolleeId Can Not be Null or Empty.");
    }
}