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

            var templateData = _db.CreateTemplateQuestionInfoes.OrderByDescending(x=>x.creationTime).ToList();
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
        public JsonResult GetTemplateDetailById()
        {
            var username = "sumitchourasia91@gmail.com";
            var id = 3;
            var response = new ResponseModel<ClientTemplateDetailById>();

            var templateData = _db.CreateTemplateQuestionInfoes.SingleOrDefault(x => x.Id == id);
            var createTemplateeditableInstructionsListsCreateResponse = _db.CreateTemplateeditableInstructionsLists.OrderBy(x => x.Id).Where(x=>x.referenceKey==templateData.referenceId).ToList();
            var createTemplateSingleQuestionsListsCreateResponse = _db.CreateTemplateSingleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
            var createTemplateMultipleQuestionsListsCreateResponse = _db.CreateTemplateMultipleQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
            var createTemplateTextBoxQuestionsListsCreateResponse = _db.CreateTemplateTextBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
            var createTemplateListBoxQuestionsListsCreateResponse = _db.CreateTemplateListBoxQuestionsLists.OrderBy(x => x.Id).Where(x => x.referenceKey == templateData.referenceId).ToList();
            
            if (templateData != null)
            {
                var createTemplateQuestionInfoModelEditableInstructionsCreateResponse = new CreateTemplateQuestionInfoModel();

                createTemplateQuestionInfoModelEditableInstructionsCreateResponse.type = "AddInstructions";
                createTemplateQuestionInfoModelEditableInstructionsCreateResponse.title = templateData.title;
                createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = false;
                createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Add Instructions";
                
                createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList = new List<CreateTemplateeditableInstructionsListModel>();
                foreach (var editableInstructionsLists in createTemplateeditableInstructionsListsCreateResponse)
                {
                    var createTemplateeditableInstructionsListModelCreateResponse = new CreateTemplateeditableInstructionsListModel
                    {
                        Number = editableInstructionsLists.Number,
                        Text = editableInstructionsLists.Text
                    };
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.editableInstructionsList.Add(createTemplateeditableInstructionsListModelCreateResponse);
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.buttonText = "Remove Instructions";
                    createTemplateQuestionInfoModelEditableInstructionsCreateResponse.visible = true;
                }

                var createTemplateQuestionInfoModelSingleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                createTemplateQuestionInfoModelSingleQuestionsCreateResponse.type = "AddSingleQuestionsList";
                createTemplateQuestionInfoModelSingleQuestionsCreateResponse.title = templateData.title;
                createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = false;
                createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Add Ques. (single Ans.)";
                
                createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList = new List<CreateTemplateSingleQuestionsListModel>();
                foreach (var singleQuestionsLists in createTemplateSingleQuestionsListsCreateResponse)
                {                  
                    var createTemplateSingleQuestionsListModelCreateResponse = new CreateTemplateSingleQuestionsListModel
                    {
                        Number = singleQuestionsLists.Number,
                        Question = singleQuestionsLists.Question,
                        Options = singleQuestionsLists.Options                       
                    };
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.singleQuestionsList.Add(createTemplateSingleQuestionsListModelCreateResponse);
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.visible = true;
                    createTemplateQuestionInfoModelSingleQuestionsCreateResponse.buttonText = "Remove Ques. (single Ans.)";
                }

                var createTemplateQuestionInfoModelMultipleQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.type = "AddMultipleQuestionsList";
                createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.title = templateData.title;
                createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = false;
                createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Add Ques. (Multiple Ans.)";
                createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList = new List<CreateTemplateMultipleQuestionsListModel>();
                foreach (var multipleQuestionsLists in createTemplateMultipleQuestionsListsCreateResponse)
                {
                    var createTemplateMultipleQuestionsListModelCreateResponse = new CreateTemplateMultipleQuestionsListModel
                    {
                        Number = multipleQuestionsLists.Number,
                        Question = multipleQuestionsLists.Question,
                        Options = multipleQuestionsLists.Options
                    };
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.multipleQuestionsList.Add(createTemplateMultipleQuestionsListModelCreateResponse);
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.visible = true;
                    createTemplateQuestionInfoModelMultipleQuestionsCreateResponse.buttonText = "Remove Ques. (Multiple Ans.)";
                }

                var createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.type = "AddTextBoxQuestionsList";
                createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.title = templateData.title;
                createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = false;
                createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Add Ques. (TextBox Ans.)";
                
                createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList = new List<CreateTemplateTextBoxQuestionsListModel>();
                foreach (var textBoxQuestionsLists in createTemplateTextBoxQuestionsListsCreateResponse)
                {
                    var createTemplateTextBoxQuestionsListModelCreateResponse = new CreateTemplateTextBoxQuestionsListModel
                    {
                        Number = textBoxQuestionsLists.Number,
                        Question = textBoxQuestionsLists.Question,
                        Options = textBoxQuestionsLists.Options
                    };
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.textBoxQuestionsList.Add(createTemplateTextBoxQuestionsListModelCreateResponse);
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.visible = true;
                    createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse.buttonText = "Remove Ques. (TextBox Ans.)";
                }

                var createTemplateQuestionInfoModelListBoxQuestionsCreateResponse = new CreateTemplateQuestionInfoModel();

                createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.type = "AddListBoxQuestionsList";
                createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.title = templateData.title;
                createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = false;
                createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Add Ques. (ListBox Ans.)";
                
                createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList = new List<CreateTemplateListBoxQuestionsListModel>();
                foreach (var listBoxQuestionsLists in createTemplateListBoxQuestionsListsCreateResponse)
                {
                    var createTemplateListBoxQuestionsListModelCreateResponse = new CreateTemplateListBoxQuestionsListModel
                    {
                        Number = listBoxQuestionsLists.Number,
                        Question = listBoxQuestionsLists.Question,
                        Options = listBoxQuestionsLists.Options
                    };
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.listBoxQuestionsList.Add(createTemplateListBoxQuestionsListModelCreateResponse);
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.visible = true;
                    createTemplateQuestionInfoModelListBoxQuestionsCreateResponse.buttonText = "Remove Ques. (ListBox Ans.)";
                }


                response.Status = 200;
                response.Message = "success";
                response.Payload = new ClientTemplateDetailById
                {
                    Data = new List<CreateTemplateQuestionInfoModel>
                    {
                        createTemplateQuestionInfoModelEditableInstructionsCreateResponse,
                        createTemplateQuestionInfoModelSingleQuestionsCreateResponse,
                        createTemplateQuestionInfoModelMultipleQuestionsCreateResponse,
                        createTemplateQuestionInfoModelTextBoxQuestionsCreateResponse,
                        createTemplateQuestionInfoModelListBoxQuestionsCreateResponse
                    }
                };
            }

            return Json(response);
            //return Json(response,JsonRequestBehavior.AllowGet);
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
