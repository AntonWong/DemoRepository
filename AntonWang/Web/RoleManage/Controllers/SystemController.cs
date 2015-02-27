using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoleManage.Models;

namespace RoleManage.Controllers
{
    public class SystemController : Controller
    {
        readonly RoleManageContext _context = new RoleManageContext();

        public ActionResult Setfunction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Setfunction(Function model)
        {

            model.Menu = new Menu {MenuId = Convert.ToInt32(Request.Form["menuId"])};
            _context.Function.Add(model);
            _context.SaveChanges();
            return View();
        }

        public ActionResult FunctionList(int id)
        {
            return Json(_context.Function.Select(s=>new{s.FunctionName}), JsonRequestBehavior.AllowGet);
        }

    }
}
