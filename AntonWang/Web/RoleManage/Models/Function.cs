using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoleManage.Models
{
    public class Function
    {
        [Key]
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public int ControllerName { get; set; }
        public string ActionName { get; set; }
        public int MenuId { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Menu Menu { get; set; }
    }
}