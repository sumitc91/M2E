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
        private readonly M2EContext _db = new M2EContext();
        public ActionResult Index()
        {
            logger.Info("Client Controller index page");  
            return View();
        }

        [HttpPost]
        public JsonResult CreateTemplate(List<CreateTemplateQuestionInfoModel> req)
        {
            var username = "sumitchourasia91@gmail.com";
            //var templateList = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.username == username);
            var latestTemplate = _db.CreateTemplateQuestionInfoes.Max(x => x.Id);            
            return Json("create Template");
        }
    }
}
