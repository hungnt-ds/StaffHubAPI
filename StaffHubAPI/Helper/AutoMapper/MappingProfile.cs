using AutoMapper;
using StaffHubAPI.DataAccess.DTOs;
using StaffHubAPI.DataAccess.Entities;

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
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<SubmissionDTO, Submission>();
            CreateMap<Submission, SubmissionDTO>();
            CreateMap<User, UserRegisterRequestDTO>().ReverseMap();
        }
    }
}
