using AutoMapper;
using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using DirectoryApp.Application.Query.ListEnrollees;
using DirectoryApp.Domain.Entity;

namespace DirectoryApp.Application.MapperProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Enrollee, ListEnrolleesViewModel>().ReverseMap();
        CreateMap<Enrollee, GetEnrolleeWithDetailViewModel>().ReverseMap();
    }
}