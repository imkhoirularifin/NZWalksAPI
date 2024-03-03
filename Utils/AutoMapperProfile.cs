using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateRegionDto, Region>().ReverseMap();
            CreateMap<CreateWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, ResponseWalkDto>().ReverseMap();

            // Example if property have different name
            //CreateMap<UserDto, User>()
            //    .ForMember(e => e.Name, opt => opt.MapFrom(e => e.FullName))
            //    .ReverseMap();
        }

        //public class UserDto
        //{
        //    public string FullName { get; set; }
        //}

        //public class User
        //{
        //    public string Name { get; set; }
        //}
    }
}
