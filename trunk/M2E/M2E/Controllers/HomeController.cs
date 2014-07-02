using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using M2E.Common.Logger;
using GaDotNet.Common.Data;
using GaDotNet.Common;
using GaDotNet.Common.Helpers;
using System.Diagnostics;
using System.Configuration;

namespace M2E.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType), Convert.ToBoolean(ConfigurationManager.AppSettings["GALogging"]));

        public ActionResult Index()
        {
            logger.Info("index page");                        
            return View();
        }

    }
}
