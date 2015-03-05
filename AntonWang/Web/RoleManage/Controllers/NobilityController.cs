﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleManage.Controllers
{
    public class NobilityController : Controller
    {
        //
        // GET: /Nobility/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sing()
        {
            return Json("Sing");
        }
        public ActionResult Dance()
        {
            return Json("Dance");
        }

    }
}
