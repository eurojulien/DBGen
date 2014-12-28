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
        public String port      { get; set; }
        public String ipAddress { get; set; }
        public String method    { get; set; }
        public String body      { get; set; }
        public String header    { get; set; }
        public DateTime dateTime{ get; set; }
    }
}