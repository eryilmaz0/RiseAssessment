using DirectoryApp.Application.Command.CreateContactInformation;
using FluentValidation;

namespace DirectoryApp.API.Validator;

public class CreateContactInformationCommandValidator : AbstractValidator<CreateContactInformationCommand>
{
    public CreateContactInformationCommandValidator()
    {
        RuleFor(x => x.EnrolleeId).NotEmpty().WithErrorCode("EnrolleeId Can Not be Null or Empty.");
        RuleFor(x => x.InformationType).IsInEnum().WithErrorCode("Information Type Can Not be Null or Empty.");
        RuleFor(x => x.Information).NotEmpty().WithErrorCode("Information Can Not be Null or Empty.");
    }
}