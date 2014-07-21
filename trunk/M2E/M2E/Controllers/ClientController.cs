using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Common.Logger;
using System.Reflection;
using System.Configuration;
using M2E.Models.DataResponse;
using M2E.Models.DataWrapper;
using M2E.Models.DataWrapper.CreateTemplate;
using M2E.Models;
using M2E.CommonMethods;
using M2E.Service.JobTemplate;

namespace M2E.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/        
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();
        public ActionResult Index()
        {
            logger.Info("Client Controller index page");  
            return View();
        }

        [HttpPost]
        public JsonResult GetAllTemplateInformation()
        {
            var username = "sumitchourasia91@gmail.com";
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.GetAllTemplateInformation(username));
        }

        [HttpPost]
        public JsonResult GetTemplateDetailById()
        {
            var username = Request.QueryString["username"].ToString();
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var response = new ResponseModel<ClientTemplateDetailById>();
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.GetTemplateDetailById(username, id));            
        }

        [HttpPost]
        public JsonResult CreateTemplate(List<CreateTemplateQuestionInfoModel> req)
        {
            var username = Request.QueryString["username"].ToString();
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.CreateTemplate(req,username));
        }

        [HttpPost]
        public JsonResult DeleteTemplateDetailById()
        {
            var username = Request.QueryString["username"].ToString();
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var response = new ResponseModel<ClientTemplateDetailById>();
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.DeleteTemplateDetailById(username, id));
        }
    }
}
