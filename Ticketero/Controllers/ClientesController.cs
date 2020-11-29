using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticketero;

namespace Ticketero.Controllers
{
    public class ClientesController : Controller
    {
        private BD_TicketEntities db = new BD_TicketEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Cliente1).Include(c => c.Cliente2).Include(c => c.Cliente11).Include(c => c.Cliente3);
            return View(cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombre,Apellido,CI")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombre,Apellido,CI")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id_Cliente", "Nombre", cliente.Id_Cliente);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
