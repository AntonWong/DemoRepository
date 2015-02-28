using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleManage.Models
{
    public class MenuView
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int ParentId { get; set; }
    }

    public class ParentSubMenuView
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int ParentId { get; set; }
        public IEnumerable<MenuView> Menus{ get; set; }
    }
}