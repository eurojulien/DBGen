using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DBGen.Models;
using Npgsql;
using System.Data;

namespace DBGen.Controllers
{
    public class RequestsController : ApiController
    {
        private Connector connector;
        private DataRecord dataRecord;
        private const String TABLE_REQUEST = "Request";

        public RequestsController()
        {
            connector   = new Connector();
            dataRecord  = new DataRecord();
        }

        ~RequestsController()
        {
        }

        public void recordRequest()
        {
            Request recordedRequest     = new Request();
            recordedRequest.port        = Request.RequestUri.Port;
            recordedRequest.ipAddress   = Request.RequestUri.AbsolutePath.ToString();
            recordedRequest.method      = Request.Method.ToString();
            recordedRequest.protocol    = Request.RequestUri.Scheme;
            recordedRequest.url         = Request.RequestUri.AbsoluteUri;
            recordedRequest.body        = Request.Content.ToString();
            recordedRequest.header      = Request.Headers.ToString();

            dataRecord.addRequest(recordedRequest, connector);
        }

        public IEnumerable<Request> GetAllRequests()
        {
            recordRequest();
            return dataRecord.getAllRecordsFromTable(TABLE_REQUEST, connector);
        }

        public IHttpActionResult getRequest(int id)
        {
            // TODO : Le code plante lorsqu'aucun record n'est retrouve
            recordRequest();
            Request request = dataRecord.getRecordFromTable(TABLE_REQUEST, id, connector);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
