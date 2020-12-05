using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticketero.Models;

namespace Ticketero.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private BD_TicketEntities db = new BD_TicketEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.Caja).Include(t => t.Cliente);
            return View(ticket.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {

            ViewBag.Id_Caja = new SelectList(db.Caja, "Id_Caja", "Descripcion");
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            return View();
        }

        // POST: Tickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id_Ticket,Nro_Ticket,Fecha,Estado,Id_Cliente,Id_Caja")] Ticket ticket)
        //public ActionResult Create([Bind(Include = "Id_Cliente")] Ticket ticket)
        public ActionResult Create([Bind(Include = "Id_Ticket, Id_Cliente")] Ticket ticket)
        //public ActionResult Create(Ticket ticket)
        {


            //verifica si hay disponibilidad de cajas
            int disponible = (from p in db.Caja
                              where p.Estado == "D"
                              orderby p.Codigo ascending
                              select p.Codigo).Count();


            if (disponible > 0)
            {
                //trae la letra de la primera caja disponible
                var linqLetraTicket = (from p in db.Caja
                                       where p.Estado == "D"
                                       orderby p.Codigo ascending
                                       select p.Codigo).Take(1);

                string letraTicket = linqLetraTicket.ToList()[0].ToString();

                //trae el id de la primera caja disponible
                var linqIdCaja = (from p in db.Caja
                                  where p.Estado == "D"
                                  orderby p.Codigo ascending
                                  select p.Id_Caja).Take(1);
                string idCaja = linqIdCaja.ToList()[0].ToString();
                int intIdCaja = Int32.Parse(idCaja);
                //trae el numero de ticket a asociar a la caja disponible
                DateTime current_date = DateTime.Now.Date;
                int linqNroTicket = (from p in db.Ticket
                                     where p.Id_Caja == intIdCaja && p.Fecha == current_date
                                     select p).Count();
                linqNroTicket++;
                string nroTicket = (linqNroTicket).ToString();

                string nroLetraTicket = letraTicket + "" + nroTicket;

                if (ModelState.IsValid)
                {
                    //ticket.Id_Ticket = ticket.Id_Ticket;
                    ticket.Id_Cliente = ticket.Id_Cliente;
                    ticket.Nro_Ticket = nroLetraTicket;
                    ticket.Id_Caja = intIdCaja;
                    ticket.Fecha = DateTime.Now;
                    ticket.Estado = "P";
                    db.Ticket.Add(ticket);
                    db.SaveChanges();

                    //actualiza estado de la caja
                    Caja caja = db.Caja.Find(intIdCaja);
                    caja.Estado = "N";
                    db.Entry(caja).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                //trae la letra de la primera caja disponible
                var linqLetraTicket = (from p in db.Caja
                                       orderby p.Codigo ascending
                                       select p.Codigo).Take(1);

                string letraTicket = linqLetraTicket.ToList()[0].ToString();

                //trae el id de la primera caja disponible
                var linqIdCaja = (from p in db.Caja
                                  orderby p.Codigo ascending
                                  select p.Id_Caja).Take(1);

                string idCaja = linqIdCaja.ToList()[0].ToString();

                int intIdCaja = Int32.Parse(idCaja);
                //trae el numero de ticket a asociar a la caja disponible
                DateTime current_date = DateTime.Now.Date;
                int linqNroTicket = (from p in db.Ticket
                                     where p.Id_Caja == intIdCaja && p.Fecha == current_date
                                     select p).Count();
                linqNroTicket++;
                string nroTicket = (linqNroTicket).ToString();

                string nroLetraTicket = letraTicket + "" + nroTicket;

                if (ModelState.IsValid)
                {
                    //ticket.Id_Ticket = ticket.Id_Ticket;
                    ticket.Id_Cliente = ticket.Id_Cliente;
                    ticket.Nro_Ticket = nroLetraTicket;
                    ticket.Id_Caja = intIdCaja;
                    ticket.Fecha = DateTime.Now;
                    ticket.Estado = "P";
                    db.Ticket.Add(ticket);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }

            ViewBag.Id_Caja = new SelectList(db.Caja, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Caja = new SelectList(db.Caja, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Ticket,Nro_Ticket,Fecha,Estado,Id_Cliente,Id_Caja")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                if (ticket.Estado.ToUpper().Equals("F"))
                {
                    //actualiza estado de la caja
                    Caja caja = db.Caja.Find(ticket.Id_Caja);
                    caja.Estado = "D";
                    db.Entry(caja).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Id_Caja = new SelectList(db.Caja, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Ticket ticket = db.Ticket.Find(id);
            Caja caja = db.Caja.Find(ticket.Id_Caja);

            db.Ticket.Remove(ticket);
            db.SaveChanges();

            //actualiza estado de la caja
            caja.Estado = "D";
            db.Entry(caja).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
        //Trae la lista diaria de Tickets
        public ActionResult DailyList()
        {
            DateTime fecha = DateTime.Now.Date;
            var ticket = db.Ticket.Include(t => t.Caja).Include(t => t.Cliente).Where(t =>  t.Fecha == fecha);

            return View(ticket.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
