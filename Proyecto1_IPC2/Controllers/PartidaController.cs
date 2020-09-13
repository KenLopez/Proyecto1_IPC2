using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Controllers
{
    public class PartidaController : Controller
    {
        // GET: Partida
        public ActionResult UnJugador()
        {
            return View();
        }

        [HttpPost]
        public void ponerFicha()
        {
             
        }
    }
}