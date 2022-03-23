using AutoMapper;
using DirectoryApp.Application.Cache;
using DirectoryApp.Application.Command.CreateEnrollee;
using DirectoryApp.Application.Command.RemoveEnrollee;
using DirectoryApp.Application.Command.RemoveEnrolleeFromDirectory;
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
    private readonly ICache _cache;

    public EnrolleeService(IEnrolleeRepository enrolleeRepository, IMapper mapper, ICache cache)
    {
        _enrolleeRepository = enrolleeRepository;
        _mapper = mapper;
        _cache = cache;
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


        //Breaking Cache
        await _cache.RemoveFromCacheAsync("enrollee");      
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


        //Breaking Cache
        await _cache.RemoveFromCacheAsync("enrollee");
        await _cache.RemoveFromCacheAsync($"enrollee_{enrollee.Id}");
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
        if(await _cache.IsKeyExistAsync("enrollees"))
        {
            return await _cache.ReadFromCacheAsync<List<ListEnrolleesViewModel>>("enrollees");
        }

        var enrollees = await _enrolleeRepository.GetAll();
        var mappedenrollees = _mapper.Map<List<ListEnrolleesViewModel>>(enrollees);

        await _cache.SetToCacheAsync<List<ListEnrolleesViewModel>>("enrollees", mappedenrollees);
        return mappedenrollees;

    }

    public async Task<GetEnrolleeWithDetailViewModel> Handle(GetEnrolleeWithDetailQuery request, CancellationToken cancellationToken)
    {
        if (await _cache.IsKeyExistAsync($"enrollee_{request.EnrolleeId}"))
        {
            return await _cache.ReadFromCacheAsync<GetEnrolleeWithDetailViewModel>($"enrollee_{request.EnrolleeId}");
        }

        var enrolleeWithDetail = await _enrolleeRepository.Find(x => x.Id == request.EnrolleeId);

        if (enrolleeWithDetail is null)
            return null;

        var mappedenrollee = _mapper.Map<GetEnrolleeWithDetailViewModel>(enrolleeWithDetail);
        await _cache.SetToCacheAsync<GetEnrolleeWithDetailViewModel> ($"enrollee_{request.EnrolleeId}", mappedenrollee);
        return mappedenrollee;
    }


    public async Task<RemoveEnrolleeFromDirectoryResponse> Handle(RemoveEnrolleeFromDirectoryCommand request, CancellationToken cancellationToken)
    {
        var enrollee = await _enrolleeRepository.Find(x => x.Id == request.EnrolleeId);

        if (enrollee is null)
            return new() { IsSuccess = false, ResultMessage = "Enrollee Not Found." };

        if(!enrollee.IsActive)
            return new() { IsSuccess = false, ResultMessage = "Enrollee is Not Active Already." };

        enrollee.IsActive = true;
        var result = await _enrolleeRepository.Update(enrollee);

        if(!result)
            return new() { IsSuccess = false, ResultMessage = "An Error Occured when Updating Enrollee." };

        return new() { IsSuccess = true, ResultMessage = "Enrollee Removed From Directory Successfully." };
    }
}