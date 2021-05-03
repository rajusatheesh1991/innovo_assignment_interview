
using InnovoAssignment.Web.Models;
using InnovoAssignment.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnovoAssignment.Web.Contracts
{
    public interface IUserService
    {
        Task<Int64BaseResponse> Authenticate(UserModel userModel);
        Task<StringBaseResponse> Register(UserModel userModel);

        Task<StringBaseResponse> UpdateProfile(UserModel userModel);

        Task<StringBaseResponse> RequestValidationCode(int id,string type);

        Task<CreateUserCommandBaseResponse> GetProfile(int id);
    }
}
