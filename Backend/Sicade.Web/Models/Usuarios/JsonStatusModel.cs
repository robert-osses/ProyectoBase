using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sicade.Web.Models.Auth
{
    public class JsonStatusModel
    {
        public int statusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
