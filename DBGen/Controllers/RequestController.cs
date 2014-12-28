using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DBGen.Models;

namespace DBGen.Controllers
{
    public class RequestController : ApiController
    {
        Request[] requests = new Request[]
        {
            new Request{}
        };

        public IEnumerable<Request> GetAllRequests()
        {
            return requests;
        }

        public IHttpActionResult getRequest(int id)
        {
            var request = requests.FirstOrDefault((p) => p.id == id);
            if(request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
