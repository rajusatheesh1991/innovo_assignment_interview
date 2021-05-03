using AutoMapper;
using InnovoAssignment.Web.Contracts;
using InnovoAssignment.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InnovoAssignment.Web.Services;
using System.Threading.Tasks;
using InnovoAssignment.Web.Responses;

namespace InnovoAssignment.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IClient serviceCleint;
        private readonly IMapper mapper;

        public UserService(IMapper mapper, IClient client)
        {
            this.serviceCleint = client;
            this.mapper = mapper;
        }

        public Task<Int64BaseResponse> Authenticate(UserModel userModel)
        {

            var model = mapper.Map<LoginModel>(userModel);
            var response = serviceCleint.AuthenticateAsync(model).Result;

            return Task.FromResult(response);

          
        }

        public Task<CreateUserCommandBaseResponse> GetProfile(int id )
        {
            
            var response = serviceCleint.GetByIdAsync(id).Result;

            return Task.FromResult(response);
        }

        public Task<StringBaseResponse> Register(UserModel userModel)
        {

            var model = mapper.Map<LoginModel>(userModel);
            var response = serviceCleint.CreateAsync(model).Result;

            return Task.FromResult(response);
        }

        public Task<StringBaseResponse> RequestValidationCode(int id, string type)
        {


            
            var response = serviceCleint.ValidateAsync(id,type).Result;

            return Task.FromResult(response);
        }

        public Task<StringBaseResponse> UpdateProfile(UserModel userModel)
        {
            var model = mapper.Map<CreateUserCommand>(userModel);
            var response = serviceCleint.UdateAsync(model).Result;

            return Task.FromResult(response);
        }
    }

        
    }

