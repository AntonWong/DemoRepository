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

            int menuId = Convert.ToInt32(Request.Form["menuId"]);
            model.Menu = _context.Menu.Where(s => s.MenuId == menuId).FirstOrDefault();
            _context.Function.Add(model);
            _context.SaveChanges();
            return View();
        }

        public ActionResult FunctionList(int id)
        {
            return Json(_context.Function.Where(s=>s.MenuId==id).Select(s=>new{s.FunctionName}), JsonRequestBehavior.AllowGet);
        }

    }
}
