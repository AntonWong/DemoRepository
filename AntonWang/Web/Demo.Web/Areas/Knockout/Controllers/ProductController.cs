﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Web.Areas.Knockout.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Knockout/Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}
