using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InnovoAssignment.Utilities;
using InnovoAssignment.Web.Contracts;
using InnovoAssignment.Web.Models;
using InnovoAssignment.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnovoAssignment.Web.Controllers
{
    public class CustomerController : Controller
    {

     
        private IMapper autoMapper;
        private readonly IUserService userService;

        public CustomerController(IMapper autoMapper, IUserService userService)
        {
           
            this.autoMapper = autoMapper;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            ViewBag.Message = TempData["response"];
            return View();
        }

        public IActionResult Profile(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(StringConstants.USER_ID)))
            {

                return RedirectToAction(nameof(Login));

            }

            ViewBag.Message = TempData["response"];
          
        

            var data = userService.GetProfile(id).Result;
            var model = autoMapper.Map<UserModel>(data.Data);

            return View(model);
        }

        public IActionResult Validation(int id)
        {

          

            var response = userService.RequestValidationCode(id, (String)TempData["type"]).Result;

            if (response.Success)
            {
                ViewBag.VerificationCode = response.Data;
                ViewBag.Id = id;
                ViewBag.Type = (String)TempData["type"];
                return View();
            }
            else
            {
                ViewBag.Message = response.Message;
                return View();
            }

           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ValidateVerificationCode(VerificationModel model)
        {
            if(model .sCode== model.vCode)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(StringConstants.USER_ID)))
                {
                    HttpContext.Session.SetInt32(StringConstants.USER_ID, model.Id);

                }

                return Json(1);
            }
            else
            {
                return Json(0);
            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateCredentials(UserModel model)
        {
           

            var response = userService.Authenticate(model).Result;

            if (response.Success)
            {
                
                    TempData["type"] = "Login";
                    return RedirectToAction(nameof(Validation), new { id = response.Data });
               
               
            }
              
            else
            {
                TempData["response"] = response.Message;
                return RedirectToAction(nameof(Login));

            }
          


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUser(UserModel model)
        {
          
            
            if (model.Update)
            {
                var response = userService.UpdateProfile(model).Result;
                if (response.Success)
                    return RedirectToAction(nameof(Profile), new { id = model.Id });
                else
                {

                    TempData["response"] = response.Message;
                    return RedirectToAction(nameof(Profile), new { id = model.Id });
                }
            }
            else
            {
                var response = userService.Register(model).Result;
                if (response.Success)
                {
                    TempData["type"] ="SignUp";
                    return RedirectToAction(nameof(Validation), new { id = response.Data });

                }
                   
                else
                {
                    TempData["response"] = response.Message;
                    return  RedirectToAction(nameof(SignUp));
                }
            }

          
        }

       

        public IActionResult Logout()
        {
            if(HttpContext.Session!=null)
            {

                if (HttpContext.Session.GetString(StringConstants.USER_ID) != null)
                {
                    HttpContext.Session.Remove(StringConstants.USER_ID);
                }
            }


            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            ViewBag.Message = TempData["response"];
            return View();
        }

    }
}