using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using OdataService.Models;


namespace ProductOdata.Controllers
{
    public class SuppliersController : ODataController
    {
        ProductsContext db = new ProductsContext();

        [EnableQuery]
        public IQueryable<Supplier> Get()
        {
            return db.Suppliers;
        }
        [EnableQuery]
        public SingleResult<Supplier> Get([FromODataUri] int key)
        {
            IQueryable<Supplier> result = db.Suppliers.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


        public HttpResponseMessage Post(Supplier supplier)
        {
            var enrty = db.Entry(supplier);
            enrty.State = EntityState.Unchanged;
            db.Configuration.ValidateOnSaveEnabled = false;
            enrty.Property("Name").IsModified = true;
            db.SaveChanges();
            return Request.CreateResponse();
            try
            { }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "保存出错了");
            }
        }

        public HttpResponseMessage Delete([FromODataUri] int key)
        {
            db.Configuration.AutoDetectChangesEnabled = false;
            var supplier = new Supplier();
            supplier.Id = key;
            DbEntityEntry<Supplier> entry = db.Entry(supplier);
            db.Configuration.ValidateOnSaveEnabled = false;
            entry.State = EntityState.Deleted;
            db.SaveChanges();
            return Request.CreateResponse();
        }

        // GET /Products(1)/Supplier
        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            var result = db.Products.Where(m => m.Id == key).Select(m => m.Supplier);
            return SingleResult.Create(result);
        }

        // Other controller methods not shown.
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}