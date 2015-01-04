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
        private String connectionPostGreSQL = "";
        private const String SERVER = "localhost";
        private const String PORT = "5432";
        private const String ROLE = "postgre";
        private const String PWORD = "role";
        private const String DB = "requests";

        private NpgsqlConnection connector;
        private NpgsqlDataAdapter dataApapter;
        private DataSet ds;
        private DataTable dt;

        private const String SQL_SELECT = "SELECT * FROM requests";

        Request[] requests = new Request[]
        {
            new Request{id=1, protocol="http", ipAddress="127.0.0.1", port="8000", method="GET", body="", header="", dateTime= new System.DateTime()},
            new Request{id=2, protocol="http", ipAddress="127.0.0.1", port="8000", method="PUT", body="", header="", dateTime= new System.DateTime()},
            new Request{id=3, protocol="http", ipAddress="127.0.0.1", port="8000", method="POST", body="", header="", dateTime= new System.DateTime()}
        };

        public RequestsController()
        {
            connectionPostGreSQL = String.Format(   "Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                                                    SERVER, PORT, ROLE, PWORD, DB);

            connector = new NpgsqlConnection(connectionPostGreSQL);
            connector.Open();

            ds = new DataSet();
            dt = new DataTable();
        }

        ~RequestsController()
        {
            connector.Close();
            connector = null;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            dataApapter = new NpgsqlDataAdapter(SQL_SELECT, connector);
            ds.Reset();
            dataApapter.Fill(ds);
            dt = ds.Tables[0];

            return (IEnumerable<Request>) dt;
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
