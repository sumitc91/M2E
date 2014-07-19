﻿using System;
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
            var response = new ResponseModel<List<ClientTemplateResponse>>();

            List<CreateTemplateQuestionInfo> templateData = _db.CreateTemplateQuestionInfoes.OrderByDescending(x=>x.creationTime).ToList();
            response.Status = 200;
            response.Message = "success";
            response.Payload = new List<ClientTemplateResponse>();
            foreach (var job in templateData)
            {
                var clientTemplate = new ClientTemplateResponse
                {
                    title = job.title,
                    creationDate = job.creationTime.Split(' ')[0],
                    showTime = " 4 hours",
                    editId = job.Id.ToString(CultureInfo.InvariantCulture),
                    showEllipse = true,
                    timeShowType = "success"
                };
                response.Payload.Add(clientTemplate);
            }
            return Json(response);
        }


        [HttpPost]
        public JsonResult CreateTemplate(List<CreateTemplateQuestionInfoModel> req)
        {
            var username = "sumitchourasia91@gmail.com";
            //var templateList = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.username == username);

            var keyInfo = _db.CreateTemplateQuestionInfoes.FirstOrDefault();
            var refKey = username;
            if (keyInfo != null)
            {
                refKey += _db.CreateTemplateQuestionInfoes.Max(x => x.Id) + 1;
            }
            else
            {
                refKey += 1;
            }

            var createTemplateQuestionsInfoInsert = new CreateTemplateQuestionInfo
            {
                buttonText = "NA",
                username = username,
                title = req[0].title,
                visible = "NA",
                type = "NA",
                creationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                referenceId = refKey,
                total = "NA",
                completed = "NA",
                verified = "NA"
            };

            foreach (var templateQuestions in req)
            {
                if (templateQuestions.visible == false)
                    continue;
                switch (templateQuestions.type)
                {
                    case "AddInstructions":
                        foreach (var instructionsList in templateQuestions.editableInstructionsList)
                        {
                            var createTemplateeditableInstructionsListInsert = new CreateTemplateeditableInstructionsList
                            {
                                username = username,
                                Number = instructionsList.Number,
                                Text = instructionsList.Text,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateeditableInstructionsLists.Add(createTemplateeditableInstructionsListInsert);
                        }
                        break;
                    case "AddSingleQuestionsList":
                        foreach (var singleQuestionList in templateQuestions.singleQuestionsList)
                        {
                            var createTemplateSingleQuestionsListInsert = new CreateTemplateSingleQuestionsList
                            {
                                username = username,
                                Number = singleQuestionList.Number,
                                Question = singleQuestionList.Question,
                                Options = singleQuestionList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateSingleQuestionsLists.Add(createTemplateSingleQuestionsListInsert);
                        }
                        break;
                    case "AddMultipleQuestionsList":
                        foreach (var multipleQuestionsList in templateQuestions.multipleQuestionsList)
                        {
                            var createTemplateMultipleQuestionsListInsert = new CreateTemplateMultipleQuestionsList
                            {
                                username = username,
                                Number = multipleQuestionsList.Number,
                                Question = multipleQuestionsList.Question,
                                Options = multipleQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateMultipleQuestionsLists.Add(createTemplateMultipleQuestionsListInsert);
                        }
                        break;
                    case "AddTextBoxQuestionsList":
                        foreach (var textBoxQuestionsList in templateQuestions.textBoxQuestionsList)
                        {
                            var createTemplateTextBoxQuestionsListInsert = new CreateTemplateTextBoxQuestionsList
                            {
                                username = username,
                                Number = textBoxQuestionsList.Number,
                                Question = textBoxQuestionsList.Question,
                                Options = textBoxQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateTextBoxQuestionsLists.Add(createTemplateTextBoxQuestionsListInsert);
                        }
                        break;
                    case "AddListBoxQuestionsList":
                        foreach (var listBoxQuestionsList in templateQuestions.listBoxQuestionsList)
                        {
                            var createTemplateListBoxQuestionsListInsert = new CreateTemplateListBoxQuestionsList
                            {
                                username = username,
                                Number = listBoxQuestionsList.Number,
                                Question = listBoxQuestionsList.Question,
                                Options = listBoxQuestionsList.Options,
                                assignTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                assignedTo = "NA",
                                completedAt = "NA",
                                referenceKey = refKey,
                                status = "Open",
                                verified = "NA"
                            };
                            _db.CreateTemplateListBoxQuestionsLists.Add(createTemplateListBoxQuestionsListInsert);
                        }
                        break;
                }
            }
            _db.CreateTemplateQuestionInfoes.Add(createTemplateQuestionsInfoInsert);
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                DbContextException.LogDbContextException(e);
            }
            return Json("create Template");
        }
    }
}
