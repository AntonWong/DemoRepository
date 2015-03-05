using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using RoleManage.Models;

namespace RoleManage.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        readonly RoleManageContext _context = new RoleManageContext();

        /// <summary>
        /// 
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_context.Role.ToList());
        }

        public ActionResult SetRole(int id)
        {
            List<Menu> list = _context.Role.Where(s => s.RoleId == id).Select(m => m.Menus).FirstOrDefault().ToList();
            //List<Menu> list = (List<Menu>) _context.Role.Where(s => s.RoleId == id).Select(m => m.Menus).FirstOrDefault();
          
          //var list =  _context.Menu.Where(s => s.Roles.Select(a => a.RoleId).Contains((id))).ToList();
          Mapper.CreateMap<Menu, MenuView>();
          var addressDtoList = AutoMapper.Mapper.Map<List<Menu>, List<MenuView>>(list);
           // var list = _context.Role.Where(a => a.RoleId == id).Select(s => s.Menus).ToList();
          var tree = list.Where(s => s.ParentId == 0).Select(a =>
                new ParentSubMenuView
                {
                    MenuId = a.MenuId,
                    MenuName = a.MenuName,
                    ControllerName = a.ControllerName,
                    ActionName = a.ActionName,
                    Menus = addressDtoList.Where(m => m.ParentId == a.MenuId)
                }).ToList();
          return View(tree);
        }

        public ActionResult Menus()
        {
            var list = _context.Menu.ToList();
            Mapper.CreateMap<Menu, MenuView>();
            var addressDtoList = AutoMapper.Mapper.Map<List<Menu>, List<MenuView>>(list);
            var tree = addressDtoList.Where(s => s.ParentId == 0).Select(a =>
                new ParentSubMenuView
                {
                    MenuId=a.MenuId,
                    MenuName = a.MenuName,
                    ControllerName = a.ControllerName,
                    ActionName = a.ActionName,
                    Menus =   addressDtoList.Where(m => m.ParentId == a.MenuId)
                }).ToList();
          //  return Json(tree, JsonRequestBehavior.AllowGet);
            return View(tree);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveModuleMenu(int roleId, List<int> menuId)
        {
            var roleMenus = _context.Role.Where(s => s.RoleId == roleId).FirstOrDefault();
            roleMenus.Menus = null;
            _context.SaveChanges();
            var roleMenuList = new List<Menu>();
            foreach (var menu in menuId)
            {
                var roleMenu = _context.Menu.Where(m => m.MenuId == menu).FirstOrDefault();
                roleMenuList.Add(roleMenu);
            }
            roleMenus.Menus = roleMenuList;
            _context.SaveChanges();
            return RedirectToAction("setrole?id=1");
        }

        public ActionResult ModuleRoleFunction()
        {
            return View();
        }

    }
}
