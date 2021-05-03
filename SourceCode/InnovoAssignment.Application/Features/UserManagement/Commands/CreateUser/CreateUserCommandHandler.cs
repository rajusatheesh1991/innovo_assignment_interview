using AutoMapper;
using Hangfire;
using InnovoAssignment.Application.Contracts.Infrastructure;
using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Application.Models;
using InnovoAssignment.Application.Responses;
using InnovoAssignment.Application.Security;
using InnovoAssignment.Domain.Entities;
using InnovoAssignment.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,
        BaseResponse<Int64>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IUserPreferencesRepository _userPreferencesRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptDecryptManager encryptDecryptManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateUserCommand> _logger;
       

        public CreateUserCommandHandler(IUserRepository userRepository,
            IUserAddressRepository userAddressRepository, IUserPreferencesRepository userPreferencesRepository,
            IMapper mapper, IEmailService emailService, ILogger<CreateUserCommand> logger,
            IEncryptDecryptManager encryptDecryptManager)
        {
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _userPreferencesRepository = userPreferencesRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            this.encryptDecryptManager = encryptDecryptManager;
        }

        public  Task<BaseResponse<Int64>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {


            var response = new BaseResponse<Int64>();
            response.Success = false;

            try
            {



                if(request.Update)
                {
                    var validator = new UpdateUserCommandValidator();
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
                }
                else
                {
                    var validator = new CreateUserCommandValidator(_userRepository);
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
                }
               
               
                    var user = _mapper.Map<User>(request);
                    if (user == null)
                    {
                        response.Message = "User Mapping Failed";
                        return Task.FromResult(response);
                    }


                   

                    if (request.Update)
                    {

                        var userFromDb = _userRepository.GetById(request.Id, false);
                        user.Password = userFromDb.Password;
                        bool updated=_userRepository.UpdateUserWithSp(user);

                       

                       
                        var address = _mapper.Map<UserAddress>(request);

                        //if (address == null)
                        //{
                        //    response.Message = "Address Mapping Failed";
                        //    return response;
                        //}

                     


                        var prefernce = _mapper.Map<UserPreferences>(request);


                        //if (prefernce == null)
                        //{
                        //    response.Message = "Prefernce Mapping Failed";
                        //    return response;
                        //}



                      
                        if(address!=null)
                        {
                            var addressFromDb = _userAddressRepository.GetByUserId(request.Id);
                            address.UserId = request.Id;
                            if (addressFromDb != null)
                            {

                                address.Id = addressFromDb.Id;
                                _userAddressRepository.CreateOrUpdate(address);
                            }
                            else
                            {
                                address.Id = 0;
                                _userAddressRepository.CreateOrUpdate(address);
                            }

                        }

                        if(prefernce!=null)
                        {
                            var preferenceFromDb = _userPreferencesRepository.GetByUserId(request.Id);
                            prefernce.UserId = request.Id;
                            if (preferenceFromDb != null)
                            {

                                prefernce.Id = preferenceFromDb.Id;
                                _userPreferencesRepository.CreateOrUpdate(prefernce);
                            }
                            else
                            {
                                prefernce.Id = 0;
                                _userPreferencesRepository.CreateOrUpdate(prefernce);
                            }
                        }



                       

                        

                        response.Message = "Profile updated successfully";
                        response.Success = true;
                    }
                    else
                    {
                        user.Password = encryptDecryptManager.Encrypt(user.Password);
                    //  var savedUser = _userRepository.AddAsync(user).Result;

                      var savedUser = _userRepository.SignupWithSp(user);
                    if (savedUser != null && savedUser.Id > 0)
                        {


                            response.Data = savedUser.Id;
                            response.Message = "Registration Successful";
                            response.Success = true;
                        SendRegistrationSuccessMail(savedUser.Id, savedUser.Email);
                     
                       // Hangfire call
                        //BackgroundJob.Enqueue(() => SendRegistrationSuccessMail(savedUser.Id,savedUser.Email));



                    }
                        else
                        {
                            response.Message = "User save failed";
                        }

                    }









                
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception occured: {e.Message}");
               
                response.Message = e.Message.ToString();
                response.Success = false;
            }

            return Task.FromResult(response);
        }

        private void SendRegistrationSuccessMail(int id,string email)
        {
            // We will confire this domain url in appSettings.json file with 
            //http://localhost:52885/Customer/VerifyAccount/"

            try
            {

                System.Uri accountVerificationLink = new System.Uri("http://tinyurl.com/api-create.php?url=" +
                        "http://localhost:52885/Customer/VerifyAccount/" +id);
                System.Net.WebClient client = new System.Net.WebClient();
                string tinyUrl = client.DownloadString(accountVerificationLink);

                EmailDto emailDto = new EmailDto
                {
                    To = email,
                    Body =
                    String.Format(StringConstants.REGISTRAION_EMAIL_BODY, tinyUrl),
                    Subject = StringConstants.REGISTRAION_EMAIL_HEADER
                };
                _emailService.SendEmail(emailDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Mailing about board failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
