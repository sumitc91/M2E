using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Session
{
    public class M2ESession
    {
        public M2ESession(string UserName)
        {
            DateTime now = DateTime.Now;
            this.SessionId = Guid.NewGuid().ToString();
            this.UserName = UserName;
        }
        public string SessionId { get; set; }
        public string UserName { get; set; }
    }
}