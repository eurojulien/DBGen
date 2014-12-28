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
            new Request{id=1, protocol="http", ipAddress="127.0.0.1", port="8000", method="GET", body="", header="", dateTime= new System.DateTime()},
            new Request{id=2, protocol="http", ipAddress="127.0.0.1", port="8000", method="PUT", body="", header="", dateTime= new System.DateTime()},
            new Request{id=3, protocol="http", ipAddress="127.0.0.1", port="8000", method="POST", body="", header="", dateTime= new System.DateTime()}
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
