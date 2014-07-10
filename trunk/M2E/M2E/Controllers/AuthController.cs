using System.Runtime.InteropServices;
using M2E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Models.DataWrapper;
using M2E.CommonMethods;
using System.Data.Entity.Validation;
using M2E.Encryption;

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
        public JsonResult Login(string id)
        {
            var returnUrl = "/";
            var referral = Request.QueryString["ref"];
            var response = new ResponseModel<string> {Status = 200, Message = "success", Payload = "1234567890"};
            System.Threading.Thread.Sleep((5000));
            return Json(response);
        }

        [HttpPost]
        public JsonResult CreateAccount(ClientRegisterationRequest req)
        {
            
            var returnUrl = "/";
            var referral = Request.QueryString["ref"];
            var response = new ResponseModel<string>();
            if (_db.Users.Any(x => x.Username == req.Username))
            {
                response.Status = 409;
                response.Message = "conflict";
                return Json(response);
            }

            var guid = Guid.NewGuid().ToString();
            EncryptionClass encrypt = new EncryptionClass();
            var user = new User
            {
                Username = req.Username,
                Password = encrypt.MD5Hash(req.Password),
                Source = req.Source,
                isActive = "false",
                Type = req.Type,
                guid = Guid.NewGuid().ToString(),
                FirstName = req.FirstName,
                LastName = req.LastName,
                gender = "NA",
                ImageUrl = "NA"
            };
            _db.Users.Add(user);

            if (!string.IsNullOrEmpty(req.Referral))
            {
                var dbRecommedBy = new RecommendedBy
                {
                    RecommendedFrom = req.Referral,
                    RecommendedTo = req.Username
                };
                _db.RecommendedBies.Add(dbRecommedBy);
            }
            if (req.Type == "client")
            {
                var dbClientDetails = new ClientDetail
                {
                    Username = req.Username,
                    CompanyName = req.CompanyName
                };
                _db.ClientDetails.Add(dbClientDetails);
            }
            var dbValidateUserKey = new ValidateUserKey
            {
                Username = req.Username,
                guid = guid
            };

            _db.ValidateUserKeys.Add(dbValidateUserKey);

            try
            {
                _db.SaveChanges();
                 SendAccountCreationValidationEmail.SendAccountCreationValidationEmailMessage(req.Username, guid, Request);
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
            response.Payload = "Account Created";
            
            return Json(response);
        }

    }
}
