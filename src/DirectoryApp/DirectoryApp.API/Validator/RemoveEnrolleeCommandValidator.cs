using DirectoryApp.Application.Command.RemoveEnrollee;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class RemoveEnrolleeCommandValidator : AbstractValidator<RemoveEnrolleeCommand>
{
    public RemoveEnrolleeCommandValidator()
    {
        RuleFor(x => x.EnrolleeId).NotEmpty().WithMessage("Enrollee Id Can Not be Null or Empty.");
    }
}