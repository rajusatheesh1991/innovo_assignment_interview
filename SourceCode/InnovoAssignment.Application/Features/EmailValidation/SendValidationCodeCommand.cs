using InnovoAssignment.Application.Contracts.Infrastructure;
using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Application.Models;
using InnovoAssignment.Application.Responses;
using InnovoAssignment.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Features.EmailValidation
{
    public class SendValidationCodeCommand:IRequest<BaseResponse<String>>
    {

        public int Id;
        public string ValidationType;

    }

    public class SendValidationCodeCommandHandler : IRequestHandler<SendValidationCodeCommand, BaseResponse<String>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<SendValidationCodeCommand> _logger;

        public SendValidationCodeCommandHandler(IUserRepository userRepository, 
            IEmailService emailService, ILogger<SendValidationCodeCommand> logger)
        {
            _userRepository = userRepository;
            this.emailService = emailService;
            _logger = logger;
        }

        public Task<BaseResponse<String>> Handle(SendValidationCodeCommand request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponse<String>();
            if (request.Id == 0)
            {
                response.Success = false;
               
                response.Message = $"Parameter {request.Id} cannot be null";
            }
            else
            if (String.IsNullOrEmpty(request.ValidationType)
               )
            {
                response.Success = false;

                response.Message = $"Parameter {request.ValidationType} cannot be null";
            }
            else
            {
                var user = _userRepository.GetById(request.Id, false);

                if(user!=null)
                {
                    var randomNumber = GetRandomNumner(6);
                    if (randomNumber != null)
                    {

                        try
                        {
                            EmailDto email =
                                new EmailDto
                                {
                                    To = user.Email,
                                    Subject =
                                 String.Format(StringConstants.VALIDATION_EMAIL_HEADER, request.ValidationType),
                                    Body = String.Format(StringConstants.VALIDATION_EMAIL_BODY,randomNumber, request.ValidationType)
                                };
                            emailService.SendEmail(email);
                            response.Success = true;
                            response.Data = randomNumber;

                            response.Message = "Verification code sent";

                        }
                        catch (Exception ex)
                        {
                            response.Success = false;

                            response.Message = $"{ex.Message}";

                            _logger.LogError($"Mailing about board failed due to an error with the mail service: {ex.Message}");
                        }
                    }
                    else
                    {
                        response.Success = false;

                        response.Message = $"Random number null";
                    }
                }
                else
                {
                    response.Success = false;

                    response.Message = $"User not found for id : {request.Id}";
                }
               
            }

            return Task.FromResult(response);
        }
        private  string GetRandomNumner(int length)
        {
            try
            {
                const string valid = "1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                return res.ToString();
            }
            catch (Exception e1)
            {
                return null;
            }
        }
    }
}
