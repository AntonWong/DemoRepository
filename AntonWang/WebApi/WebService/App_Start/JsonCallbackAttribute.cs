using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace WebService.App_Start
{
    public class JsonCallbackAttribute : ActionFilterAttribute
    {
        private const string CallbackQueryParameter = "callback";

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string callback;
            if (IsJosnp(out callback))
            {
                var jsonBuilder = new StringBuilder(callback);
                jsonBuilder.AppendFormat("({0})", actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
                actionExecutedContext.Response.Content = new StringContent("C(\"a\")");
            }
            base.OnActionExecuted(actionExecutedContext);
        }

        private bool IsJosnp(out string callback)
        {
            callback = System.Web.HttpContext.Current.Request.QueryString[CallbackQueryParameter];
            return !string.IsNullOrEmpty(callback);
        }

    }
}
