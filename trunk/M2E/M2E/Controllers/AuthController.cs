using System.Runtime.InteropServices;
using M2E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Models.DataResponse;
using M2E.Models.DataWrapper;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using M2E.Encryption;
using M2E.Service;
using M2E.Service.Register;

namespace M2E.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/        
        private readonly M2EEntities _db = new M2EEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginRequest req)
        {
            var returnUrl = "/";
            var referral = Request.QueryString["ref"];
            var responseData = new LoginResponse();
            if (req.Type == "web")
            {
                var loginService = new LoginService();
                responseData = loginService.WebLogin(req.UserName, req.Password, returnUrl, req.KeepMeSignedInCheckBox != null ? "true" : "false");                
            }

            var response = new ResponseModel<LoginResponse> { Status = 200, Message = "success", Payload = responseData };
            return Json(response);
        }

        [HttpPost]
        public JsonResult CreateAccount(RegisterationRequest req)
        {
            var returnUrl = "/";
            var referral = Request.QueryString["ref"];
            var response = new ResponseModel<string>();
            if (req.Source != "web") return Json("Not Web");
            var webRegisterService = new WebRegister();
            return Json(webRegisterService.WebRegisterService(req, Request));
        }

        [HttpPost]
        public JsonResult validateAccount(ValidateAccountRequest req)
        {
            var response = new ResponseModel<string>();
            if (_db.ValidateUserKeys.Any(x => x.Username == req.userName && x.guid == req.guid))
            {
                User User = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                User.isActive = "true";
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    return Json(response);
                }
                response.Status = 200;
                response.Message = "validated";
                return Json(response);
            }
            else
            {
                response.Status = 402;
                response.Message = "link expired";
                return Json(response);
            }

        }

    }
}
