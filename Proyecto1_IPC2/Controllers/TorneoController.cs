using Proyecto1_IPC2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1_IPC2.Models;

namespace Proyecto1_IPC2.Controllers
{
    public class TorneoController : Controller
    {
        static Models.ViewModels.Torneo torneo;
        static Models.ViewModels.usuarioViewModel usuario;
        // GET: Torneo
        public ActionResult Equipos(usuarioViewModel creador)
        {
            usuario = creador;
            torneo = new Models.ViewModels.Torneo(creador, TempData["nombre"].ToString(), Int32.Parse(TempData["cantidad"].ToString()));
            return View();
        }
    }
}