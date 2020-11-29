using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ticketero.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema de Gestion de Ticket simple, para Gestion de Turnos Diarios por Caja.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactenos.";

            return View();
        }
    }
}