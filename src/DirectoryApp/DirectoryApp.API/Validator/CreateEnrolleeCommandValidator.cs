using DirectoryApp.Application.Command.CreateEnrollee;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class CreateEnrolleeCommandValidator : AbstractValidator<CreateEnrolleeCommand>
{
    public CreateEnrolleeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Can Not be Null or Empty.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName Can Not be Null or Empty.");
        RuleFor(x => x.Firm).NotEmpty().WithMessage("Firm Can Not be Null or Empty.");
    }
}