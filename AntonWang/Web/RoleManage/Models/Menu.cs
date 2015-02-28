using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace RoleManage.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int ParentId { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Function> Functions { get; set; }
    }
}