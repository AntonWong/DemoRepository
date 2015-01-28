using System.Web.Mvc;

namespace Demo.Web.Areas.Knockout
{
    public class KnockoutAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Knockout";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Knockout_default",
                "Knockout/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
