using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebService.App_Start;

namespace WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());
            GlobalConfiguration.Configure(WebApiConfig.Register);

           
           // GlobalConfiguration.Configuration.Filters.Add(new JsonCallbackAttribute());
        }
    }
}
