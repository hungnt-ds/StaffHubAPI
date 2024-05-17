using AutoMapper;
using StaffHubAPI.DataAccess.Entities;
using StaffHubAPI.DTOs;

namespace StaffHubAPI.Helper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserRespondDTO>().ReverseMap();
            CreateMap<User, UserUpdateRequestDTO>()
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null)); ;
            //.ForMember(dest => dest.NameOfCaffe, opt => opt.MapFrom(src => src.Name));
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<SubmissionDTO, Submission>();
            CreateMap<Submission, SubmissionDTO>();
            CreateMap<User, UserRegisterRequestDTO>().ReverseMap();
        }
    }
}
