using AutoMapper;
using InnovoAssignment.Web.Models;
using InnovoAssignment.Web.Services;

namespace InnovoAssignment.Web.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, CreateUserCommand>().ReverseMap();

          //  CreateMap<UserModel, AuthenticateUserQuery>().ReverseMap();



            CreateMap<UserModel, LoginModel>().ReverseMap();
            CreateMap<UserModel, CreateUserCommandBaseResponse>().ReverseMap();
        }
    }
}
