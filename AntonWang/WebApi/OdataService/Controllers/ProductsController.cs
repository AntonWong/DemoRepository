using System;
using System.Net.Http;
using System.Web;
using OdataService.Models;
using ProductOdata.Migrations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Tools;

namespace ProductOdata.Controllers
{
    public class ProductsController : ODataController
    {
        ProductsContext db = new ProductsContext();
        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.Id == key);
        }

        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return db.Products;
        }
      

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = db.Products.Where(p => p.Id == key);
           
            return SingleResult.Create(result);
        }
        
        // 关联查询 GET /Products(1)/Supplier
        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            var result = db.Products.Where(m => m.Id == key).Select(m => m.Supplier);
            return SingleResult.Create(result);
        }
        //[EnableQuery]
        //public IQueryable<Product> GetProducts([FromODataUri] int key)
        //{
        //    return db.Suppliers.Where(m => m.Id.Equals(key)).SelectMany(m => m.Products);
        //}
        public HttpResponseMessage Post(Product product)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "message");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> CreateRef([FromODataUri] int key,
            string navigationProperty, [FromBody] Uri link)
        {
            var product = await db.Products.SingleOrDefaultAsync(p => p.Id == key);
            if (product == null)
            {
                return NotFound();
            }
            switch (navigationProperty)
            {
                case "Supplier":
                    // Note: The code for GetKeyFromUri is shown later in this topic.
                    var relatedKey = Helpers.GetKeyFromUri<int>(Request, link);
                    var supplier = await db.Suppliers.SingleOrDefaultAsync(f => f.Id == relatedKey);
                    if (supplier == null)
                    {
                        return NotFound();
                    }

                    product.Supplier = supplier;
                    break;

                default:
                    return StatusCode(HttpStatusCode.NotImplemented);
            }
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public HttpResponseMessage Rate([FromODataUri] int key)
        {
            var data = HttpContext.Current.Request.Form;
            if (!ModelState.IsValid)
            {
                //return BadRequest();
            }
             OperationResult result = new OperationResult(OperationResultType.Success,"message");
             return Request.CreateResponse(HttpStatusCode.OK, result);
            //return new HttpResponseMessage(HttpStatusCode.OK,result);
            //int rating = (int)parameters["Rating"];
            //db.Ratings.Add(new ProductRating
            //{
            //    ProductID = key,
            //  //  Rating = rating
            //});

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateException e)
            //{
            //    if (!ProductExists(key))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
        }



        //public async Task<IHttpActionResult> Post(Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    db.Products.Add(product);
        //    await db.SaveChangesAsync();
        //    return Created(product);
        //}
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Products.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            product.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Product update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.Id)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}