using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnovoAssignment.Application.Features.EmailValidation;
using InnovoAssignment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnovoAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValidationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("requestcode",Name = "Validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<String>>> Validate(int id,string type)
        {
            var response = await _mediator.Send(new SendValidationCodeCommand()
            {
                Id=id,
                ValidationType=type

            });
            return Ok(response);
        }
    }
}