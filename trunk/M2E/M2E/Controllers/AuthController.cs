using M2E.Common.Logger;
using M2E.Models;
using System;
using System.Linq;
using M2E.Encryption;
using System.Web.Mvc;
using M2E.Models.DataResponse;
using M2E.Models.DataWrapper;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using M2E.Service;
using M2E.Service.Register;
using System.Reflection;

namespace M2E.Controllers
{
    public class AuthController : Controller
    {
        // GET: /Auth/        
        private readonly M2EContext _db = new M2EContext();
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
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
                responseData = loginService.WebLogin(req.UserName, EncryptionClass.Md5Hash(req.Password), returnUrl, req.KeepMeSignedInCheckBox != null ? "true" : "false");                
            }

            var response = new ResponseModel<LoginResponse> { Status = Convert.ToInt32(responseData.Code), Message = "success", Payload = responseData };
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
                var User = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                if (User == null)
                {
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    Logger.Info("Validate Account : " + req.userName);
                    return Json(response);
                }
                if (User.isActive == "true")
                {
                    response.Status = 402;
                    response.Message = "link expired";
                    return Json(response);
                }
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
            response.Status = 402;
            response.Message = "link expired";
            return Json(response);
        }

    }
}
