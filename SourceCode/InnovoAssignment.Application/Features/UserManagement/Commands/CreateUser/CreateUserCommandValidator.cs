using FluentValidation;
using InnovoAssignment.Application.Contracts.Persistence;
using System;

namespace InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository userRepository;
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            
                RuleFor(p => p.Email)
              .NotNull().WithMessage("{PropertyName} is required.")
              .NotEmpty().When(p => p.Email!=null).WithMessage("{PropertyName} is required.")
              .MaximumLength(60).When(p => !String.IsNullOrEmpty(p.Email)).WithMessage("{PropertyName} must not exceed 60 characters.")
          .Must(BeUniqueEmail).When(p => !String.IsNullOrEmpty(p.Email)).WithMessage("{PropertyValue} already registered. please register with another email");

                RuleFor(p => p.Password)
                   .NotNull().WithMessage("{PropertyName} is required.")
                     .NotEmpty().When(p => p.Password != null).WithMessage("{PropertyName} is required.")
                   .Length(4).When(p => !String.IsNullOrEmpty(p.Password)).WithMessage("{PropertyName} must be 4 characters.");


              
            



        }




        private bool BeUniqueEmail(string emailId)
        {
            var data = userRepository.GetByEmail(emailId);

            return data==null?true:false;
        }

    }
}