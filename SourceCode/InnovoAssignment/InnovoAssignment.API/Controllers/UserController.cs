using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnovoAssignment.API.Models;
using InnovoAssignment.Application.Features.EmailValidation;
using InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser;
using InnovoAssignment.Application.Features.UserManagement.Queries;
using InnovoAssignment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnovoAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup",Name = "Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<String>>> Create([FromBody] LoginModel model)
        {
            var response = await _mediator.Send(new CreateUserCommand()
            {
                Email= model.Email,
                Password= model.Password

            });
            return Ok(response);
        }


        [HttpGet("profile", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<CreateUserCommand>>> GetById(int id)
        {
            var response = await _mediator.Send(new GetProfileQuery()
            {
                Id = id

            }) ;
            return Ok(response);
        }

        [HttpPut("profile", Name = "udate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<String>>> Update([FromBody] CreateUserCommand model)
        {
            var response = await _mediator.Send(model);
            return Ok(response);
        }

    }
}