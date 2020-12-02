using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ticketero.Models;

namespace Ticketero.Controllers
{
    public class CajasApiController : ApiController
    {
        private BD_TicketEntities db = new BD_TicketEntities();

        // GET: api/CajasApi
        public IQueryable<Caja> GetCaja()
        {
            return db.Caja;
        }

        // GET: api/CajasApi/5
        [ResponseType(typeof(Caja))]
        public IHttpActionResult GetCaja(int id)
        {
            Caja caja = db.Caja.Find(id);
            if (caja == null)
            {
                return NotFound();
            }

            return Ok(caja);
        }

        // PUT: api/CajasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCaja(int id, Caja caja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caja.Id_Caja)
            {
                return BadRequest();
            }

            db.Entry(caja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CajaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CajasApi
        [ResponseType(typeof(Caja))]
        public IHttpActionResult PostCaja(Caja caja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Caja.Add(caja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = caja.Id_Caja }, caja);
        }

        // DELETE: api/CajasApi/5
        [ResponseType(typeof(Caja))]
        public IHttpActionResult DeleteCaja(int id)
        {
            Caja caja = db.Caja.Find(id);
            if (caja == null)
            {
                return NotFound();
            }

            db.Caja.Remove(caja);
            db.SaveChanges();

            return Ok(caja);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CajaExists(int id)
        {
            return db.Caja.Count(e => e.Id_Caja == id) > 0;
        }
    }
}