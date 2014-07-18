using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Common.Logger;
using System.Reflection;
using System.Configuration;
using M2E.Models.DataWrapper;
using M2E.Models.DataWrapper.CreateTemplate;
using M2E.Models;

namespace M2E.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private readonly M2EEntities _db = new M2EEntities();
        public ActionResult Index()
        {
            logger.Info("Client Controller index page");  
            return View();
        }

        [HttpPost]
        public JsonResult CreateTemplate(List<CreateTemplateQuestionInfoModel> req)
        {
            var username = "sumitchourasia91@gmail.com";
            
            return Json("create Template");
        }
    }
}
