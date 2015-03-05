using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoleManage.Models;

namespace RoleManage.Controllers
{
    public class MenuController : Controller
    {
        RoleManageContext context = new RoleManageContext();
        private string userName = "'Jack'";
        public ActionResult Index()
        {
          
            string sql = @" select Menus.MenuId,Menus.MenuName,Menus.ParentId,Menus.ControllerName,Menus.ActionName
                            from Menus Menus,RoleMenus RoleMenus,Roles Roles,UserRole UserRole,Users Users 
                            where Menus.MenuId=RoleMenus.Menu_MenuId and Roles.RoleId=RoleMenus.Role_RoleId and Users.UserId=UserRole.User_UserId 
                            and Users.UserName=" + userName;
         //  var list = context.Database.SqlQuery<MenuView>(sql).ToList();
            var list = context.Menu.Select(s => new MenuView
            {
                MenuId = s.MenuId,
                MenuName = s.MenuName,
                ControllerName = s.ControllerName,
                ActionName = s.ActionName,
                ParentId = s.ParentId
            }).ToList();
            var tree = list.Where(s => s.ParentId == 0).Select(a =>
                new ParentSubMenuView
                {
                    MenuName = a.MenuName,
                    ControllerName = a.ControllerName,
                    ActionName = a.ActionName,
                    Menus = list.Where(m => m.ParentId == a.MenuId)
                }).ToList();
           return View(tree);
        }

        //public ActionResult Module()
        //{
        //    return context.Menu
        //}

    }
}
