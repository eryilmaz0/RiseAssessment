using DirectoryApp.Application.Command.RemoveContactInformation;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class RemoveContactInformationCommandValidator : AbstractValidator<RemoveContactInformationCommand>
{
    public RemoveContactInformationCommandValidator()
    {
        RuleFor(x => x.InformationId).NotEmpty().WithMessage("Informatıon Id Can Not be Null or Empty.");
    }
}