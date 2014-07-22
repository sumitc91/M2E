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
using M2E.Service.Client;

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
        public JsonResult GetTemplateImageDetailById()
        {
            var username = Request.QueryString["username"].ToString();
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var response = new ResponseModel<ClientTemplateDetailById>();
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.GetTemplateImageDetailById(username, id));
        }

        [HttpPost]
        public JsonResult CreateTemplate(CreateTemplateRequest req)
        {
            var username = Request.QueryString["username"].ToString();
            var TemplateList = req.Data;
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            var CreateTemplateResponse = ClientTemplate.CreateTemplate(TemplateList, username);            
            var ImgurImageList = req.ImgurList;         
            if (CreateTemplateResponse.Status == 200)
            {
                if (ImgurImageList != null)
                    ClientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(ImgurImageList, username, CreateTemplateResponse.Payload);
            }
            
            return Json(CreateTemplateResponse);
        }        

        [HttpPost]
        public JsonResult CreateTemplateWithId(CreateTemplateRequest req)
        {
            var username = Request.QueryString["username"].ToString();
            var id = Request.QueryString["id"].ToString();
            var TemplateList = req.Data;
            var ImgurImageList = req.ImgurList;
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            if(ImgurImageList !=null)
            ClientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(ImgurImageList,username,id);
            return Json(ClientTemplate.CreateTemplateWithId(TemplateList, username, id));
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

        [HttpPost]
        public JsonResult DeleteTemplateImgurImageById()
        {
            var username = Request.QueryString["username"].ToString();
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var response = new ResponseModel<ClientTemplateDetailById>();
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            return Json(ClientTemplate.DeleteTemplateImgurImageById(username, id));
        }

        [HttpPost]
        public JsonResult EditTemplateDetailById(CreateTemplateRequest req)
        {
            var username = Request.QueryString["username"].ToString();
            var id = Convert.ToInt32(Request.QueryString["id"]);
            var TemplateList = req.Data;
            ClientTemplateService ClientTemplate = new ClientTemplateService();
            var CreateTemplateResponse = ClientTemplate.EditTemplateDetailById(TemplateList,username, id);
            var ImgurImageList = req.ImgurList;
            var refKey = username+id;
            if (CreateTemplateResponse.Status == 200)
            {
                if (ImgurImageList != null)
                    ClientTemplate.ImgurImagesSaveToDatabaseWithTemplateId(ImgurImageList, username, refKey);
            }
            
            return Json(CreateTemplateResponse);
        }

        [HttpPost]
        public JsonResult GetClientDetails()
        {
            var username = Request.QueryString["username"].ToString();
            ClientDetailService ClientTemplate = new ClientDetailService();
            var ClientDetailResponse = ClientTemplate.GetClientDetails(username);
            return Json(ClientDetailResponse);
        }
    }
}
