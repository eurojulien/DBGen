using DBGen.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DBGen.Controllers
{
    public class DataRecord
    {
        private const String SQL_SELECT = "SELECT * FROM requests";
        private const String SQL_SELECT_ID = SQL_SELECT + " WHERE id = ";
        private const String SQL_INSERT = "INSERT INTO requests ";

        private NpgsqlDataAdapter dataApapter;
        private NpgsqlCommand command;

        private DataSet data;

        public DataRecord()
        {
            data = new DataSet();
        }

        ~DataRecord()
        { }

        public IEnumerable<Request> getAllRecordsFromTable(String tableName, Connector connector)
        {
            dataApapter = new NpgsqlDataAdapter(SQL_SELECT, connector.getConnector());
            data.Reset();
            dataApapter.Fill(data,tableName);

            return getRequestsFromDataSet(data, tableName);
        }

        public Request getRecordFromTable(String tableName, int id, Connector connector)
        {
            dataApapter = new NpgsqlDataAdapter(SQL_SELECT_ID + id, connector.getConnector());
            data.Reset();
            dataApapter.Fill(data, tableName);

            // TODO : Le code plante lorsqu'aucun record n'est retrouve
            return getRequestsFromDataSet(data, tableName).First<Request>();
        }
        
        public void addRequest(Request request, Connector connector)
        {
            String sqlstr = SQL_INSERT + request.SQLColumnToAffect() + " VALUES " + request.SQLValuesToInsert();
            command = new NpgsqlCommand(sqlstr, connector.getConnector());
            command.ExecuteNonQuery();
        }

        private IEnumerable<Request> getRequestsFromDataSet(DataSet ds, String tableName)
        {
            return ds.Tables[tableName].AsEnumerable().Select(row => new Request
            {
                id = row.Field<int>("id"),
                method = row.Field<String>("method"),
                port = row.Field<int>("port"),
                protocol = row.Field<String>("protocol"),
                ipAddress = row.Field<String>("ipaddress"),
                url = row.Field<String>("url"),
                header = row.Field<String>("header"),
                body = row.Field<String>("body"),
                dateTime = row.Field<DateTime>("datetime").ToLocalTime().ToString()
            });
        }
    }
}