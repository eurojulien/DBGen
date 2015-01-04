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

        private const String TABLE_REQUEST = "Request";

        private NpgsqlConnection connector;
        private NpgsqlDataAdapter dataApapter;
        private DataSet ds;
        private DataTable dt;

        private const String SQL_SELECT = "SELECT * FROM requests";
        private const String SQL_SELECT_ID = SQL_SELECT + " WHERE id = ";

        private List<Request> lrequests;

        Request[] requests = new Request[]
        {
            new Request{id=1, protocol="http", ipAddress="127.0.0.1", port=8000, method="GET", body="", header="", dateTime= new System.DateTime()},
            new Request{id=2, protocol="http", ipAddress="127.0.0.1", port=8000, method="PUT", body="", header="", dateTime= new System.DateTime()},
            new Request{id=3, protocol="http", ipAddress="127.0.0.1", port=8000, method="POST", body="", header="", dateTime= new System.DateTime()}
        };

        public RequestsController()
        {
            connectionPostGreSQL = String.Format(   "Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                                                    SERVER, PORT, ROLE, PWORD, DB);

            connector = new NpgsqlConnection(connectionPostGreSQL);
            connector.Open();

            dt = new DataTable();
            lrequests = new List<Request>();
        }

        ~RequestsController()
        {
            connector.Close();
            connector = null;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            dataApapter = new NpgsqlDataAdapter(SQL_SELECT, connector);
            ds = new DataSet();
            ds.Reset();
            dataApapter.Fill(ds,TABLE_REQUEST);

            return getRequestsFromDataSet(ds);
        }

        public IHttpActionResult getRequest(int id)
        {
            dataApapter = new NpgsqlDataAdapter(SQL_SELECT_ID + id, connector);
            ds = new DataSet();
            ds.Reset();
            dataApapter.Fill(ds, TABLE_REQUEST);

            // TODO : Le code plante lorsqu'aucun record n'est retrouve
            Request request = getRequestsFromDataSet(ds).First<Request>();

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        private IEnumerable<Request> getRequestsFromDataSet(DataSet ds)
        {
            return ds.Tables[TABLE_REQUEST].AsEnumerable().Select(row => new Request
            {
                id = row.Field<int>("id"),
                method = row.Field<String>("method"),
                port = row.Field<int>("port"),
                protocol = row.Field<String>("protocol"),
                ipAddress = row.Field<String>("ipAddress"),
                url = row.Field<String>("url"),
                header = row.Field<String>("header"),
                body = row.Field<String>("body"),
                dateTime = row.Field<DateTime>("dateTime")
            });
        }

    }
}
