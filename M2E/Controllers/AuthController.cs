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

    }
}
