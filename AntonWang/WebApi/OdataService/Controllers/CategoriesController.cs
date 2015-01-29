using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using OdataService.Models;
using ProductOdata;

namespace OdataService.Controllers
{
    public class CategoriesController : ODataController
    {
        ProductsContext db = new ProductsContext();

        [EnableQuery]
        public IQueryable<Category> Get()
        {
            return db.Categories;
        }
        [EnableQuery]
        public SingleResult<Category> Get([FromODataUri] int key)
        {
            IQueryable<Category> result = db.Categories.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}