namespace PluginMvc.Framework.Mvc
{
    /// <summary>
    /// </summary>
    public class PluginRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        /// <summary>
        ///     定义区域视图页所在地址。
        /// </summary>
        private readonly string[] _areaViewLocationFormats =
        {
            "~/Plugins/{2}/Views/{1}/{0}.cshtml",
            "~/Plugins/{2}/Views/Shared/{0}.cshtml",
            "~/{2}/Views/{1}/{0}.cshtml",
            "~/{2}/Views/Shared/{0}.cshtml"
        };

        /// <summary>
        ///     定义视图页所在地址。
        /// </summary>
        private readonly string[] _viewLocationFormats =
        {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml"
        };

        public PluginRazorViewEngine()
        {
            AreaViewLocationFormats = _areaViewLocationFormats;
            AreaMasterLocationFormats = _areaViewLocationFormats;
            AreaPartialViewLocationFormats = _areaViewLocationFormats;

            ViewLocationFormats = _viewLocationFormats;
            MasterLocationFormats = _viewLocationFormats;
            PartialViewLocationFormats = _viewLocationFormats;
        }
    }
}