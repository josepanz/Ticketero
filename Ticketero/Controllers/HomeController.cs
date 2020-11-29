using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticketero.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace Ticketero.Controllers
{
    public class HomeController : Controller
    {
        BD_TicketEntities db = new BD_TicketEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Queremos escucharte";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Id_Contacto,Fecha,Nombre,Correo,Telefono,Descripcion")] Contacto contacto)
        {
            
            if (ModelState.IsValid)
            {
                contacto.Fecha = DateTime.Now; //verificar
                db.Contactoes.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}