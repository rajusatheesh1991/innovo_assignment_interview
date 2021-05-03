using AutoMapper;
using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser;
using InnovoAssignment.Application.Responses;
using InnovoAssignment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Features.UserManagement.Queries
{
    public class GetProfileQuery :IRequest<BaseResponse<CreateUserCommand>>
    {

        public int Id { get; set; }

    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, BaseResponse<CreateUserCommand>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<BaseResponse<CreateUserCommand>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var data = _userRepository.GetById(request.Id, true);

            var dto = new CreateUserCommand();

             _mapper.Map<User, CreateUserCommand>(data, dto);
             _mapper.Map<UserAddress, CreateUserCommand>(data.UserAddress, dto);
            _mapper.Map<UserPreferences, CreateUserCommand>(data.UserPreferences, dto);


            var response = new BaseResponse<CreateUserCommand>();
            response.Data = dto;
            response.Message = "Success";
            response.Success = true;

            return Task.FromResult(response);

        }
    }
}
