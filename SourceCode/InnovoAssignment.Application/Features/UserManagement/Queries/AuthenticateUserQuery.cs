using AutoMapper;
using FluentValidation;
using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Application.Responses;
using InnovoAssignment.Application.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Features.UserManagement.Queries
{
    public class AuthenticateUserQuery : IRequest<BaseResponse<Int64>>
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, BaseResponse<Int64>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptDecryptManager encryptDecryptManager;

        public AuthenticateUserQueryHandler(IUserRepository userRepository, IMapper mapper,
            IEncryptDecryptManager encryptDecryptManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this.encryptDecryptManager = encryptDecryptManager;
        }

        public Task<BaseResponse<long>> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<long>();
            response.Success = false;


            try
            {
                var validator = new AuthenticateUserQueryValidator();
                var validationResult = validator.ValidateAsync(request).Result;
                if (validationResult.Errors.Count > 0)
                {

                    response.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }


                    response.Message = String.Join("\n", response.ValidationErrors);
                  
                    return Task.FromResult(response);


                }
                else
                {
                    var userFromDb = _userRepository.GetByEmail(request.Email);

                    if (userFromDb != null)
                    {

                        if (encryptDecryptManager.Verify(request.Password, userFromDb.Password))
                        {
                            response.Success = true;
                            response.Data = userFromDb.Id;
                            response.Message = "Authentication successful";
                        }
                        else
                        {
                         
                            response.Message = "Invalid Credentials";

                        }

                    }
                    else
                    {
                        
                        response.Message = $"Registration details not found for email {request.Email}";

                    }

                }
            }
            catch(Exception e)
            {

                response.Message = e.Message.ToString();
               
            }
           
            return Task.FromResult(response);

        }


    }

        public class AuthenticateUserQueryValidator:AbstractValidator<AuthenticateUserQuery>
        {

        public AuthenticateUserQueryValidator()
        {
            RuleFor(p => p.Email)
             .NotNull().WithMessage("{PropertyName} is required.")
              .NotEmpty().When(p => p.Email != null).WithMessage("{PropertyName} is required.")
             .MaximumLength(60).When(p => String.Empty != p.Email).WithMessage("{PropertyName} must not exceed 60 characters.");

            RuleFor(p => p.Password)
           .NotNull().WithMessage("{PropertyName} is required.")
              .NotEmpty().When(p => p.Password != null).WithMessage("{PropertyName} is required.")
           .MaximumLength(4).When(p => String.Empty != p.Password).WithMessage("{PropertyName} must be 4 characters.");

        }

    }
}
