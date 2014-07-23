using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models.DataResponse
{
    public class LoginResponse
    {
        public string AuthToken { get; set; }
        public string AuthKey { get; set; }
        public string AuthValue { get; set; }
        public string TimeStamp { get; set; }
        public string Code { get; set; }
    }
}