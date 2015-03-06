using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleManage
{
   

    public class RoleBLL
    {
        RoleManageContext _context = new RoleManageContext();

        public bool IsAuthority(string controllerName, string actionName)
        {
            return false;
        }
    }
}