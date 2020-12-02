using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ticketero.Controllers
{
    public class ClientesApiController : ApiController
    {
        // GET: api/ClientesApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ClientesApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ClientesApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ClientesApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClientesApi/5
        public void Delete(int id)
        {
        }
    }
}
