using M2E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M2E.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

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
            System.Threading.Thread.Sleep((3000));
            return Json(response);
        }

    }
}
