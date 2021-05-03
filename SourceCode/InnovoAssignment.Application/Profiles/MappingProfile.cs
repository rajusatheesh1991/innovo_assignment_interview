using AutoMapper;
using InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser;
using InnovoAssignment.Application.Features.UserManagement.Queries;
using InnovoAssignment.Domain.Entities;

namespace InnovoAssignment.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<UserAddress, CreateUserCommand>().ReverseMap();
            CreateMap<UserPreferences, CreateUserCommand>().ReverseMap();

            CreateMap<User, GetProfileQuery>().ReverseMap();
            CreateMap<UserAddress, GetProfileQuery>().ReverseMap();
            CreateMap<UserPreferences, GetProfileQuery>().ReverseMap();

            
            
        }
    }
}
