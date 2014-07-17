using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataWrapper.CreateTemplate
{    
    public class CreateTemplateQuestionInfo
    {
        public string type { get; set; }
        public bool visible {get;set;}
        public string buttonText {get;set;}
        public List<CreateTemplateeditableInstructionsList> editableInstructionsList { get; set; }
        public List<CreateTemplateTextBoxQuestionsList> textBoxQuestionsList { get; set; }
        public List<CreateTemplateSingleQuestionsList> singleQuestionsList { get; set; }
        public List<CreateTemplateMultipleQuestionsList> multipleQuestionsList { get; set; }
        public List<CreateTemplateListBoxQuestionsList> listBoxQuestionsList { get; set; }
        
    }
}