using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using M2E.Common.Logger;

namespace M2E.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index()
        {
            //logger.Info("index page");
            return View();
        }

    }
}
