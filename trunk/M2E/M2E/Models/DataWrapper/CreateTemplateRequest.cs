using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models.DataWrapper.CreateTemplate;

namespace M2E.Models.DataWrapper
{
    public class CreateTemplateRequest
    {
        public List<CreateTemplateQuestionInfo> Data { get; set; }
    }    
}