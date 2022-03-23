using DirectoryApp.Application.Command.UpdateEnrollee;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class UpdateEnrolleeCommandValidator : AbstractValidator<UpdateEnrolleeCommand>
{
    public UpdateEnrolleeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can Not be Null or Empty.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Can Not be Null or Empty.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName Can Not be Null or Empty.");
        RuleFor(x => x.Firm).NotEmpty().WithMessage("Firm Can Not be Null or Empty.");
    }
}