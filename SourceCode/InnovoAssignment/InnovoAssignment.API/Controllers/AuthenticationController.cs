using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnovoAssignment.API.Models;
using InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser;
using InnovoAssignment.Application.Features.UserManagement.Commands.UpdateAccountVerification;
using InnovoAssignment.Application.Features.UserManagement.Queries;
using InnovoAssignment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnovoAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login", Name = "Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<long>>> Authenticate([FromBody]  LoginModel model)
        {
            var response = await _mediator.Send(new AuthenticateUserQuery()
            {

                Email = model.Email,
                Password = model.Password
            });
            return Ok(response);
        }

        [HttpGet("verified", Name = "AccountVerified")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<long>>> AccountVerified(int id)
        {
            var response = await _mediator.Send(new UpdateAccountVerificationCommand(){

                IsAccountVerified=true,
                Id=id
                
            });
            return Ok(response);
        }
    }
}