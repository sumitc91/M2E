using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2E.Common.Logger;
using System.Reflection;
using System.Configuration;

namespace M2E.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index()
        {
            logger.Info("Client Controller index page");  
            return View();
        }

    }
}
