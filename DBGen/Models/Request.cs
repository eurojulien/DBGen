using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBGen.Models
{
    public class Request
    {
        public int id           { get; set; }
        public String protocol  { get; set; }
        public int port         { get; set; }
        public String ipAddress { get; set; }
        public String method    { get; set; }
        public String url       { get; set; }
        public String body      { get; set; }
        public String header    { get; set; }
        public String dateTime  { get; set; }

        public String SQLValuesToInsert()
        {
            return "(" +
                   quoteStringForSQL(protocol) + ", " +
                   quoteStringForSQL(port) + ", " +
                   quoteStringForSQL(ipAddress) + ", " +
                   quoteStringForSQL(method) + ", " +
                   quoteStringForSQL(url) + ", " +
                   quoteStringForSQL(body) + ", " +
                   quoteStringForSQL(header) +
                   ")";
        }

        public String SQLColumnToAffect()
        {
            return "(" +
                   "protocol, " +
                   "port, " +
                   "ipaddress, " +
                   "method, " +
                   "url, " +
                   "body, " +
                   "header " +
                   ")";
        }

        private String quoteStringForSQL(String value)
        {
            return "\'" + value.ToString() + "\'";
        }

        private String quoteStringForSQL(int value)
        {
            return value.ToString();
        }

        private String quoteStringForSQL(DateTime value)
        {
            return quoteStringForSQL(value.ToString());
        }
    }
}