
using System.Net.Http.Formatting;
using OdataService.Models;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.Http.Cors;
namespace ProductOdata
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //CORS
            var cors = new EnableCorsAttribute("http://localhost:5027,http://localhost:52103", "*", "*");
            config.EnableCors(cors);

            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Product>("Products");
            // New code:
            builder.Namespace = "ProductService";
            builder.EntityType<Product>()
                .Action("Rate")
                .Parameter<int>("Rating");

            // New code:
            builder.EntitySet<Supplier>("Suppliers");
            config.MapODataServiceRoute("ODataRoute", null, builder.GetEdmModel());

           
            builder.Namespace = "ProductService";
            builder.EntityType<Product>()
                .Action("Rate")
                .Parameter<int>("Rating");

            config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json"); 
        }
    }
}
