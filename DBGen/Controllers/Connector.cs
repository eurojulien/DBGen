using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBGen.Controllers
{
    public class Connector
    {
        private String connectionPostGreSQL = "";

        private NpgsqlConnection connector;

        private const String SERVER = "localhost";
        private const String PORT = "5432";
        private const String ROLE = "postgre";
        private const String PWORD = "role";
        private const String DB = "requests";

        public Connector()
        {
            connectionPostGreSQL = String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                                        SERVER, PORT, ROLE, PWORD, DB);

            connector = new NpgsqlConnection(connectionPostGreSQL);
            connect();
        }

        ~Connector()
        {
            disconnect();
            connector = null;
        }

        public NpgsqlConnection getConnector()
        {
            return this.connector;
        }

        private void connect()
        {
            connector.Open();
        }

        private void disconnect()
        {
            connector.Close();
        }
    }

}