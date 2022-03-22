using DirectoryApp.Application.Command.CreateContactInformation;
using DirectoryApp.Application.Command.RemoveContactInformation;
using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;

namespace DirectoryApp.Application.Service;

public class ContactInformationService : IContactInformationService
{
    private readonly IContactInformationRepository _contactInformationRepository;
    private readonly IEnrolleeRepository _enrolleeRepository;

    public ContactInformationService(IContactInformationRepository contactInformationRepository, IEnrolleeRepository enrolleeRepository)
    {
        _contactInformationRepository = contactInformationRepository;
        _enrolleeRepository = enrolleeRepository;
    }

    public async Task<CreateContactInformationResponse> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
    {
        //Is Enrollee Exist?
        var enrollee = await _enrolleeRepository.Find(x => x.Id == request.EnrolleeId);

        if (enrollee is null)
            return new() { IsSuccess = false};

        ContactInformation newInformation = new()
        {
            InformationType = request.InformationType,
            Information = request.Information,
            EnrolleeId = request.EnrolleeId
        };

        var result = await _contactInformationRepository.Insert(newInformation);

        if (!result)
            return new() { IsSuccess = false};

        return new() { IsSuccess = true };


    }


    public async Task<RemoveContactInformationResponse> Handle(RemoveContactInformationCommand request, CancellationToken cancellationToken)
    {
        var contactInformation = await _contactInformationRepository.Find(x => x.Id == request.InformationId);

        if (contactInformation is null)
            return new() { IsSuccess = false, ResultMessage = "Contact Information Not Found." };

        var result = await _contactInformationRepository.Remove(contactInformation);

        if(!result)
            return new() { IsSuccess = false, ResultMessage = "Contact Information Not Removed." };

        return new() { IsSuccess = true, ResultMessage = "Contact Information Removed Successfully." };
    }
}