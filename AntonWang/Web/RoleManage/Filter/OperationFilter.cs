using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CypApp.Bll.Controlls.Filter
{
    /// <summary>
    /// 功能页面过滤
    /// </summary>
    public class OperationFilterAttribute : AuthorizeAttribute
    {
        private int userId = 1;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
             var routeValues = filterContext.RequestContext.RouteData.Values;
            if (!routeValues.ContainsKey("controller") || !routeValues.ContainsKey("action"))
                return;

            string controllerName = routeValues["controller"].ToString();
            string actionName = routeValues["action"].ToString();
            if (controllerName.ToLower().Equals("account") && actionName.ToLower().Equals("login"))
            {
                
            }

              
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeValues = httpContext.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("controller") && routeValues.ContainsKey("action"))
            {
                string controllerName = routeValues["controller"].ToString();
                string actionName = routeValues["action"].ToString();
                if (controllerName.ToLower().Equals("account") && actionName.ToLower().Equals("login"))
                {
                    return true;
                }
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.Redirect("/account/login");
                return true;
            }
            return true;
        }
     
    }
}