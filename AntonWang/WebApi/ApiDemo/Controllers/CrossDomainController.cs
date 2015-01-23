using System.Collections.Generic;
using System.Web.Http;
using Tools;

namespace ApiDemo.Controllers
{
    public class CrossDomainController : ApiController
    {
        [HttpGet]
        public ViewModel1 Get(string userName,string password)
        {
            return new ViewModel1 { UserName = userName, PassWord = password };
        }

        [HttpPost]
        public OperationResult Post(ViewModel1 model)
        {
            return new OperationResult(OperationResultType.ParamError, "message", model);
        }
    }

    public class ViewModel1
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}