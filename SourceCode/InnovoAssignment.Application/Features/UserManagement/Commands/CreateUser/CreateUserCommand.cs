using InnovoAssignment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponse<Int64>>
    {
        public String FullName { get; set; }

        public int Id { get; set; }

        public String PhoneNumber { get; set; }

        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public bool EnablePromotionNotifications { get; set; }
        public bool Update { get; set; }
    }
}
