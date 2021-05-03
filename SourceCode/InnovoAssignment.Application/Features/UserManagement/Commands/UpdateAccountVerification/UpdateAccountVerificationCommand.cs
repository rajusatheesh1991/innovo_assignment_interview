using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Features.UserManagement.Commands.UpdateAccountVerification
{
    public class UpdateAccountVerificationCommand : IRequest<BaseResponse<int>>
    {

        public bool IsAccountVerified { get; set; }
        public int Id { get; set; }

    }


    public class UpdateAccountVerificationCommandHandler : IRequestHandler<UpdateAccountVerificationCommand,
      BaseResponse<int>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateAccountVerificationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<BaseResponse<int>> Handle(UpdateAccountVerificationCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<int>();
            response.Success = false;
            try
            {
                
                if (request.Id > 0)
                {
                    response.Success = true;
                    var user = _userRepository.GetById(request.Id, false);

                    if(!user.IsAccountVerified)
                    {
                        user.IsAccountVerified = request.IsAccountVerified;
                        _userRepository.Update(user);
                      
                        response.Message = "Account verified";
                    }
                    else
                    {
                        response.Message = "Account already verified";
                    }

                   

                   
                    
                }
                else
                {
                   
                    response.Message = "Id cannot be zero";
                }

            }
            catch (Exception e)
            {
             
                response.Message = "Id cannot be zero";
            }

            return Task.FromResult(response);
        }
    }

}