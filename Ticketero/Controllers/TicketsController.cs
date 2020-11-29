﻿using System;
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
    public class TicketsController : Controller
    {
        private BD_TicketEntities db = new BD_TicketEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Caja).Include(t => t.Cliente);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.Id_Caja = new SelectList(db.Cajas, "Id_Caja", "Descripcion");
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Ticket,Nro_Ticket,Fecha,Estado,Id_Cliente,Id_Caja")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Caja = new SelectList(db.Cajas, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Caja = new SelectList(db.Cajas, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Ticket,Nro_Ticket,Fecha,Estado,Id_Cliente,Id_Caja")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Caja = new SelectList(db.Cajas, "Id_Caja", "Descripcion", ticket.Id_Caja);
            ViewBag.Id_Cliente = new SelectList(db.Clientes, "Id_Cliente", "Nombre", ticket.Id_Cliente);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
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