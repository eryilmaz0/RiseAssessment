using AutoMapper;
using DirectoryApp.Application.Command.CreateEnrollee;
using DirectoryApp.Application.Command.RemoveEnrollee;
using DirectoryApp.Application.Command.UpdateEnrollee;
using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using DirectoryApp.Application.Query.ListEnrollees;
using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;

namespace DirectoryApp.Application.Service;

public class EnrolleeService : IEnrolleeService
{
    private readonly IEnrolleeRepository _enrolleeRepository;
    private readonly IMapper _mapper;

    public EnrolleeService(IEnrolleeRepository enrolleeRepository, IMapper mapper)
    {
        _enrolleeRepository = enrolleeRepository;
        _mapper = mapper;
    }

    public async Task<CreateEnrolleeResponse> Handle(CreateEnrolleeCommand request, CancellationToken cancellationToken)
    {
        Enrollee newEnrollee = new()
        {
            Name = request.Name,
            LastName = request.LastName,
            Firm = request.LastName
        };

        var result = await _enrolleeRepository.Insert(newEnrollee);

        if (!result)
            return new() { IsSuccess = false };

        return new() { CreatedEnrolleeId = newEnrollee.Id };
    }


    public async Task<UpdateEnrolleeResponse> Handle(UpdateEnrolleeCommand request, CancellationToken cancellationToken)
    {
        //Is Enrollee Exist?
        var enrollee = await _enrolleeRepository.Find(x => x.Id == request.Id);

        if (enrollee is null)
            return new() { IsSuccess = false, ResultMessage = "Enrollee Not Found." };

        enrollee.Name = request.Name;
        enrollee.LastName = request.LastName;
        enrollee.Firm = request.Firm;

        var result = await _enrolleeRepository.Update(enrollee);

        if (!result)
            return new() { IsSuccess = false, ResultMessage = "Enrollee Not Updated." };

        return new() { IsSuccess = true, ResultMessage = "Enrolle Updated Successfully." };
    }



    public async Task<RemoveEnrolleeResponse> Handle(RemoveEnrolleeCommand request, CancellationToken cancellationToken)
    {
        //Is Enrollee Exist?
        var enrollee = await _enrolleeRepository.Find(x => x.Id == request.EnrolleeId);

        if (enrollee is null)
            return new() { IsSuccess = false, ResultMessage = "Enrollee Not Found." };

        var result = await _enrolleeRepository.Remove(enrollee);

        if (!result)
            return new() { IsSuccess = false, ResultMessage = "Enrollee Not Deleted." };

        return new() { IsSuccess = true, ResultMessage = "Enrolle Deleted Successfully." };
    }



    public async Task<ICollection<ListEnrolleesViewModel>> Handle(ListEnrolleesQuery request, CancellationToken cancellationToken)
    {
        var enrollees = await _enrolleeRepository.GetAll();
        return _mapper.Map<List<ListEnrolleesViewModel>>(enrollees);

    }

    public async Task<GetEnrolleeWithDetailViewModel> Handle(GetEnrolleeWithDetailQuery request, CancellationToken cancellationToken)
    {
        var enrolleeWithDetail = await _enrolleeRepository.Find(x => x.Id == request.EnrolleeId);

        if (enrolleeWithDetail is null)
            return null;

        return _mapper.Map<GetEnrolleeWithDetailViewModel>(enrolleeWithDetail);
    }
}