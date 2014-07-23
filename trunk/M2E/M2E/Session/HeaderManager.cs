using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Session
{
    public class HeaderManager
    {
        public HeaderManager(HttpRequestBase requestHeader)
        {
            IEnumerable<string> headerValues = requestHeader.Headers.GetValues("AuthToken");
            this.AuthToken = headerValues.FirstOrDefault();
            //guid = guid.Replace("/", "");
            headerValues = requestHeader.Headers.GetValues("AuthKey");
            this.AuthKey = headerValues.FirstOrDefault();

            headerValues = requestHeader.Headers.GetValues("AuthValue");
            this.AuthValue = headerValues.FirstOrDefault();
        }

        public string AuthToken { get; set; }
        public string AuthKey { get; set; }
        public string AuthValue { get; set; }
    }
}