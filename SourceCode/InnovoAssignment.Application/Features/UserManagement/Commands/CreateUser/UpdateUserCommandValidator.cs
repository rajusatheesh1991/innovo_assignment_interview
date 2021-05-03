using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.FullName)
                 .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.PhoneNumber)
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.AddressLine1)
              .NotNull().WithMessage("{PropertyName} is required.");



            RuleFor(p => p.City)
              .NotNull().WithMessage("{PropertyName} is required.");


            RuleFor(p => p.State)
              .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ZipCode)
              .NotNull().WithMessage("{PropertyName} is required.");



            RuleFor(p => p.Country)
              .NotNull().WithMessage("{PropertyName} is required.");
              
        }

    }
}
