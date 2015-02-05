using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class MessageHeaderController:Controller
    {
        public ActionResult Index()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}