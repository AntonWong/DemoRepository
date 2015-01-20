using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:3701", headers: "*", methods: "*")]
    public class TestController : ApiController
    {

        public object Get()
        {
            return new {id=1,name="wong"};
        }
        //public HttpResponseMessage Get()
        //{
        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent("GET: Test message")
        //    };
        //}

        public object Post()
        {
            return new { id = 1, name = "HsutonWong" };
        }

        //public HttpResponseMessage Post()
        //{
        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent("POST: Test message")
        //    };
        //}

        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }
    }
}