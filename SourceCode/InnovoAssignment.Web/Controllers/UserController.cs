//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using InnovoAssignment.Application.Features.EmailValidation;
//using InnovoAssignment.Application.Features.UserManagement.Commands.CreateUser;
//using InnovoAssignment.Application.Features.UserManagement.Queries;
//using InnovoAssignment.Utilities;
//using InnovoAssignment.Web.Models;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace InnovoAssignment.Web.Controllers
//{
//    public class UserController : Controller
//    {

//        private IMediator mediatR;
//        private IMapper autoMapper;

//        public UserController(IMediator mediatR, IMapper autoMapper)
//        {
//            this.mediatR = mediatR;
//            this.autoMapper = autoMapper;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult SignUp()
//        {
//            ViewBag.Message = TempData["response"];
//            return View();
//        }

//        public IActionResult Profile(int id)
//        {
//            if (string.IsNullOrEmpty(HttpContext.Session.GetString(StringConstants.USER_ID)))
//            {

//                return RedirectToAction(nameof(Login));

//            }

//            ViewBag.Message = TempData["response"];
          
//            var query = new GetProfileQuery()
//            {
//                Id=id
//            };

           

//            var data = mediatR.Send(query).Result;
//            var model = autoMapper.Map<CreateUserCommand, UserModel>(data.Data);

//            return View(model);
//        }

//        public IActionResult Validation(int id)
//        {
            
//            var cmd = new SendValidationCodeCommand()
//            {
//                Id=id,
//                ValidationType = (String)TempData["type"]

//            };

//            var response = mediatR.Send(cmd).Result;

//            if(response.Success)
//            {
//                ViewBag.VerificationCode = response.Data;
//                ViewBag.Id = id;
//                ViewBag.Type = (String)TempData["type"];
//                return View();
//            }
//            else
//            {
//                ViewBag.Message = response.Message;
//                return View();
//            }

           
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public JsonResult ValidateVerificationCode(VerificationModel model)
//        {
//            if(model .sCode== model.vCode)
//            {
//                if (string.IsNullOrEmpty(HttpContext.Session.GetString(StringConstants.USER_ID)))
//                {
//                    HttpContext.Session.SetInt32(StringConstants.USER_ID, model.Id);

//                }

//                return Json(1);
//            }
//            else
//            {
//                return Json(0);
//            }



//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult ValidateCredentials(UserModel model)
//        {
//            var query = autoMapper.Map<AuthenticateUserQuery>(model);


//            var response = mediatR.Send(query).Result;


//            if (response.Success)
//            {
                
//                    TempData["type"] = "Login";
//                    return RedirectToAction(nameof(Validation), new { id = response.Data });
               
               
//            }
              
//            else
//            {
//                TempData["response"] = response.Message;
//                return RedirectToAction(nameof(Login));

//            }
          


//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult SaveUser(UserModel model)
//        {
//            var command = autoMapper.Map<CreateUserCommand>(model);


//            var response = mediatR.Send(command).Result;

//            if(model.Update)
//            {
//                if (response.Success)
//                    return RedirectToAction(nameof(Profile), new { id = model.Id });
//                else
//                {

//                    TempData["response"] = response.Message;
//                    return RedirectToAction(nameof(Profile), new { id = model.Id });
//                }
//            }
//            else
//            {
//                if (response.Success)
//                {
//                    TempData["type"] ="SignUp";
//                    return RedirectToAction(nameof(Validation), new { id = response.Data });

//                }
                   
//                else
//                {
//                    TempData["response"] = response.Message;
//                    return  RedirectToAction(nameof(SignUp));
//                }
//            }

          
//        }

       

//        public IActionResult Logout()
//        {
//            if(HttpContext.Session!=null)
//            {

//                if (HttpContext.Session.GetString(StringConstants.USER_ID) != null)
//                {
//                    HttpContext.Session.Remove(StringConstants.USER_ID);
//                }
//            }


//            return RedirectToAction(nameof(Login));
//        }
//        public IActionResult Login()
//        {
//            ViewBag.Message = TempData["response"];
//            return View();
//        }

//    }
//}