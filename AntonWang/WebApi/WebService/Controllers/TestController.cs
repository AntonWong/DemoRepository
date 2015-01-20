using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:3701", headers: "*", methods: "*")]
    public class TestController : ApiController
    {

        public A Get()
        {
            return new A { Id = 1, Name = "wong" };
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

    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}