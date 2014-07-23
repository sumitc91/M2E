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
using M2E.Session;

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
                responseData = loginService.WebLogin(req.UserName, EncryptionClass.Md5Hash(req.Password), returnUrl, req.KeepMeSignedInCheckBox);                
            }

            if (responseData.Code == "200")
            {
                M2ESession session = new M2ESession(req.UserName);
                TokenManager.CreateSession(session);
                responseData.AuthToken = session.SessionId;
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
        public JsonResult ValidateAccount(ValidateAccountRequest req)
        {
            var response = new ResponseModel<string>();
            if (_db.ValidateUserKeys.Any(x => x.Username == req.userName && x.guid == req.guid))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                if (user == null)
                {
                    response.Status = 500;
                    response.Message = "Internal Server Error";
                    Logger.Info("Validate Account : " + req.userName);
                    return Json(response);
                }
                if (user.isActive == "true")
                {
                    response.Status = 402;
                    response.Message = "link expired";
                    return Json(response);
                }
                user.isActive = "true";
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

        [HttpPost]
        public JsonResult ResendValidationCode(ValidateAccountRequest req)
        {
            var response = new ResponseModel<string>();
            if (_db.Users.Any(x => x.Username == req.userName))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                if (user != null && (user.isActive == "true"))
                {
                    // Account has been already validated.
                    response.Status = 402;
                    response.Message = "warning";
                    return Json(response);
                }
                var guidAlreadyExist = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == req.userName);
                if (guidAlreadyExist != null)
                {
                    _db.ValidateUserKeys.Remove(guidAlreadyExist);
                }
                var dbValidateUserKey = new ValidateUserKey
                {
                    guid = Guid.NewGuid().ToString(),
                    Username = req.userName
                };
                _db.ValidateUserKeys.Add(dbValidateUserKey);
                try
                {
                    _db.SaveChanges();
                    SendAccountCreationValidationEmail.SendAccountCreationValidationEmailMessage(req.userName, dbValidateUserKey.guid, Request);
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error !!!";
                    return Json(response);
                }
                response.Status = 200;
                response.Message = "success";
                return Json(response);
            }
            // User Doesn't Exist
            response.Status = 404;
            response.Message = "warning";
            return Json(response);
        }


        public JsonResult ForgetPassword(string id)
        {
            if (_db.Users.Any(x => x.Username == id))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == id);
                if (user != null && (user.isActive == "false"))
                {
                    return Json(402, JsonRequestBehavior.AllowGet);
                }
                var forgetPasswordDataAlreadyExists = _db.ForgetPasswords.SingleOrDefault(x => x.Username == id);
                if (forgetPasswordDataAlreadyExists != null)
                    _db.ForgetPasswords.Remove(forgetPasswordDataAlreadyExists);
                var guid = Guid.NewGuid().ToString();
                var forgetPasswordData = new ForgetPassword
                {
                    Username = id,
                    guid = guid
                };
                _db.ForgetPasswords.Add(forgetPasswordData);
                try
                {
                    _db.SaveChanges();
                    var forgetPasswordValidationEmail = new ForgetPasswordValidationEmail();
                    forgetPasswordValidationEmail.SendForgetPasswordValidationEmailMessage(id, guid, Request);
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    return Json(500, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(404, JsonRequestBehavior.AllowGet);
            }
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateForgetPassword()
        {
            var response = new ResponseModel<string>();
            var guid = Request.QueryString["guid"];
            var username = Request.QueryString["username"];

            if (!_db.Users.Any(x => x.Username == username))
            {
                response.Status = 505;
                response.Message = "User Not Found.";
                Logger.Info("Search User for Forget Password : " + username);
                return Json(response);
            }
            if (_db.ForgetPasswords.Any(x => x.Username == username && x.guid == guid))
            {
                var removeForgetPasswordData = _db.ForgetPasswords.SingleOrDefault(x => x.Username == username);
                _db.ForgetPasswords.Remove(removeForgetPasswordData);

                var userData = _db.Users.SingleOrDefault(x => x.Username == username);
                if (userData != null)
                {
                    var password = EncryptionClass.Md5Hash(Request.QueryString["confirmpassword"]);
                    userData.Password = password;
                    userData.Locked = "false";
                }
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    DbContextException.LogDbContextException(e);
                    response.Status = 500;
                    response.Message = "Internal Server Error.";
                    Logger.Info("Save new Reseted Password : " + username);
                    return Json(response);
                }
                response.Status = 200;
                response.Message = "Success";
                return Json(response);
            }
            response.Status = 402;
            response.Message = "link expired";
            return Json(response);
        }
    }
}
